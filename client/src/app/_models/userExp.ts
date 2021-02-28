import { Profession } from "./profession";

export interface UserExp {
    id: number,
    srNo: number,
    professionId: number,
    employer: string,
    position: string,
    salaryCurrency: string,
    monthlySalaryDrawn: number,
    workedFrom: Date,
    workedUpto: Date
}