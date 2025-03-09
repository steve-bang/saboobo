export interface NominatimResponse {
    display_name: string;
    address: {
        house_number?: string;
        road?: string;
        neighbourhood?: string;
        suburb?: string;
        city?: string;
        county?: string;
        state?: string;
        country?: string;
        postcode?: string;
    };
}

export async function getAddressFromLatLong(lat: number, long: number): Promise<NominatimResponse | null> {
    const url = `https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${long}`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data: NominatimResponse = await response.json();

        return data // Returns the full address as a string
    } catch (error) {
        console.error("Error fetching address data:", error);
        return null;
    }
}
