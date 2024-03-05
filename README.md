# Meet4You



## Getting started

Start project
1. git clone https://gitlab.com/otus-dreamteam/meet4you.git
2. cd .\meet4you
3. docker-compose up -d
4. dotnet ef database update --startup-project WebApp/WebApp.csproj
5. cd .\frontend\
6. npm install && npm run dev

В самом начале надо установить 8-й dotnet core SDK, а также добавить поддержку EF командой

dotnet tool install --global dotnet-ef --version 8.*

Можно не исползовать ключ --global, если на компе используются разные версии EF

Тогда надо выполнить две команды:

dotnet new tool-manifest

dotnet tool install --local dotnet-ef --version 8.*

и уже после этого заработает команда

dotnet ef database update --startup-project WebApp/WebApp.csproj


## Add your files

```
cd existing_repo
git remote add origin https://gitlab.com/otus-dreamteam/meet4you.git
git branch -M main
git push -uf origin main
```
## License
MIT





