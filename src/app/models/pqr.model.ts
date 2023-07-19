import { Byte } from "@angular/compiler/src/util";

export interface PQRModel {
    id: string;
    countryId: string;
    caseTypeId: string;
    userTypeId: string;
    razonSocial: string;
    nit: string;
    cedula: string;
    name: string;
    email: string;
    phoneNumber: string;
    file: Byte;
    comentario: string;
    autorizaTratamientoDatos: boolean;
    caseNumber: number;
    caseStatus: string;
    date: string;
  }