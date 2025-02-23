import { IResponseApiType } from "@/types/Common";
import { IMediaType } from "@/types/Media";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as string;

export interface DeleteMediaParams {
    fileUrl: string;
}

export const uploadFile = async (file: File) => {

    console.log("Uploading file...")
    
    try {

        const formData = new FormData();
        formData.append("file", file);

        const response = await fetch(`${apiUrl}/api/v1/medias`, {
            method: "POST",
            body: formData,
        });

        console.log("Response", response);

        //if (!response.ok) throw new Error("Failed to upload file");

        const responseData: IResponseApiType<IMediaType> = await response.json();

        return responseData.data;
    }
    catch (error) {
        console.error("Error when upload file",error);
        throw new Error("Failed to upload file");
    }
}

export const deleteFile = async (
    params: DeleteMediaParams
) => {
    const response = await fetch(`${apiUrl}/api/v1/medias`, {
        method: "DELETE",
        body: JSON.stringify(params),
    });

    if (!response.ok) throw new Error("Failed to delete file");

    const responseData: IResponseApiType<string> = await response.json();

    return responseData.data;
}
