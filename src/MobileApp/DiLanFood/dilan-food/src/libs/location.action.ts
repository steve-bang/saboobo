import { ICity } from "@/types/address";


// export const GetAllProvinces = async () => {
//     const url = `https://provinces.open-api.vn/api/?depth=3`;
//     try {
//         const response = await fetch(url, {
//             method: 'GET',
//         });
//         if (!response.ok) {
//             throw new Error(`HTTP error! status: ${response.status}`);
//         }

//         const data : IProvince[] = await response.json();

//         return data;

//     } catch (error) {

//         console.error("Error fetching provinces data:", error);
//         return null;
//     }

// }

export const GetAllCities = async () => {
    
    const response = await fetch('https://raw.githubusercontent.com/steve-bang/vietnam-address-json-data/refs/heads/main/city.json');

    const data: ICity[] = await response.json();


    return data;

}

export const GetDistrictByCityName = async (cities : ICity[], cityName: string) => {

    const city = cities.find(city => city.name === cityName);

    const response = await fetch('https://raw.githubusercontent.com/steve-bang/vietnam-address-json-data/refs/heads/main/district.json');

    const data = await response.json();


    return data.filter((district : any) => district.city_code === city?.code);
}

export const GetWardByDistrictName = async (districts : any, districtName: string) => {

    const district = districts.find((district : any) => district.name === districtName);

    const response = await fetch('https://raw.githubusercontent.com/steve-bang/vietnam-address-json-data/refs/heads/main/ward.json');

    const data = await response.json();

    return data.filter((ward : any) => ward.district_code === district?.code);
}
