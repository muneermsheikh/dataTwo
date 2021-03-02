import { Photo } from "./photo";

export interface Member {
    id: number;
    applicationNo: number;
    userType: string;
    gender: string;
    firstName: string;
    secondName: string;
    familyName: string;
    associateId: number;
    fullName: string;
    companyName: string;
    city: string;
    country: string;
    age: number;
    username: string;
    knownAs: string;
    aadharNo: string;
    passportNo: string;
    userProfessions: string;
    userPhones: string;
    photoUrl: string;
    introduction: string;
    lookingFor: string;
    interests: string;
    photos: Photo[];
    created: Date;
    lastActive: Date;
  }
  