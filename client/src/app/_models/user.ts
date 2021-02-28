import { Address } from "./userAddress";
import { UserExp } from "./userExp";
import { UserPassport } from "./userPassport";
import { UserPhone } from "./userPhone";
import { UserProfession } from "./userProfession";
import { UserQualification } from "./userQualification";

export interface User {
    firstName: string;
    secondName: string;
    familyName: string;
    username: string;
    token: string;
    photoUrl: string;
    knownAs: string;
    gender: string;
    roles: string[];
    userRole: string;
    aadharNo: string;
    userProfessions: UserProfession[];
    userAddresses: Address[];
    userPhones: UserPhone[];
    userQualifications: UserQualification[];
    userPassports: UserPassport[];
    userExperiences: UserExp[];
    interests: string;
    lookingFor: string;
}

