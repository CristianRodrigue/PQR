

export interface FileModel{
    id: string,
    height: number,
    timestamp: string,
    uri: string,
    fileName: string,
    data:Uint8Array,
}