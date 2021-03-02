import { AgencySpecialty } from "./agencySpecialty";
import { CustomerIndustry } from "./customerIndustry";
import { CustomerOfficial } from "./customerOfficial";

export interface Customer {
    id: number;
    customerName: string;
    knownAs: string;
    customerType: string;
    add: string;
    add2: string;
    city: string;
    pin: string;
    state: string;
    country: string;
    introducedBy: string;
    isActive: boolean;
    customerIndustries: CustomerIndustry[];
    agencySpecialties: AgencySpecialty[];
    customerOfficials: CustomerOfficial[];
}