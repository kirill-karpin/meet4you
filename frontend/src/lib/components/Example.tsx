import {useCallback, useState} from "react";

export default function Example()  {
    const [count, setCount] = useState(0);
    
    const handleIncrease = useCallback(() => {
        console.log(count);
        setCount(count + 1);
    }, [count]);
    
    
    
    return <>
        <div>
            Корзина : {count} товаров
        </div>
        <form action="">
        <input type="text"/>
        <button onClick={handleIncrease} type={"submit"}>отправить</button>
        </form>
    </>;
}
