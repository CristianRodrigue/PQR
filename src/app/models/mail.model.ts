export interface MailModel{
    id:string,
    name:string,
    description:string,
    html:string,
    message?:string,
    enabled:boolean,
}