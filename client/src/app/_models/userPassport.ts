export interface UserPassport {
    id: number,
    passportNo: string,
    issuedAt: string,
    issuedOn: Date,
    validity: Date,
    isValid: boolean,
    ecnr: boolean
}