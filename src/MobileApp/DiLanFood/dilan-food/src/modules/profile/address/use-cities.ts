import { GetAllCities } from "@/libs/location.action";
import { useQuery } from "@tanstack/react-query";



export function useCities() {
    return useQuery({
        queryKey: ['cities'],
        queryFn: async () => await GetAllCities(),
    })
}