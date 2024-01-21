export default function LocationSelect()  {
    const cities: string[] = [
        'Москва',
        'Санк-Петербург'
    ];
    
    return <select>
        <option></option>
        {cities.map((item) =>  <option>{item}</option>)}
       
    </select>;
}
