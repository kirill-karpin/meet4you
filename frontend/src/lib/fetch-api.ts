import { ApiError, ApiErrors } from './errors';
interface IAppError {
  code: string;
  status: number;
  message: string;
  moreInfo?: string;
  stack?: string;
  data?: any;
}

class Api {
  token: string | null | undefined;

  get isAuthorized() {
    return Boolean(this.token);
  }

  public fetch<T>(params: {
    url: string;
    method?: string;
    body?: { [key: string]: any };
    responseTypeCheck?: ((a: any) => boolean)[];
  }): Promise<T> {
    const { url, method = 'GET', body = null } = params;
    return window
      .fetch(url, {
        headers: this.headers,
        method,
        body: body ? JSON.stringify(body) : null,
        credentials: 'include',
      })
      .then((response: Response) => {
        if (!response.ok) {
          return this.processError(response);
        }
        return response.json();
      });
  }

  public setToken(token: string | null) {
    this.token = token;
  }

  private get headers() {
    const headers: { [key: string]: string } = {
      'Content-Type': 'application/json',
    };

    if (this.token) {
      headers['Authorization'] = `Bearer ${this.token}`;
    }

    return headers;
  }

  private processError(response: any) {
    if (!response.ok) {
      for (const errorClass in ApiErrors) {
        const apiClass = (ApiErrors as any)[errorClass];
        if (apiClass.status === response.status) {
          const apiErrorClass = (ApiErrors as any)[errorClass];
          return response.json().then(({ error }: { error: IAppError }) => {
            throw new apiErrorClass(error.message, error.code);
          });
        }
      }

      if (response instanceof TypeError && response.message === 'Network request failed') {
        throw new ApiErrors.NetworkError(response.message);
      }

      return response.json().then(({ error }: { error: IAppError }) => {
        throw new ApiError(error.status, error.message);
      });
    }

    return response;
  }
  
  public signIn(params : any) {
    console.log("sign in", params)
  }
}

export const api = new Api();
