import { User } from "./user";

export class UserParams {
    gender: string;
    minAge = 18;
    maxAge = 60;
    nameLike: string;
    userType='candidate';
    status: string;
    associateId: number[];
    professionLike: string;
    industryLike:string;
    pageNumber = 1;
    pageSize = 4;
    orderBy = 'lastActive';

    constructor(user: User) {
        // this.gender = user.gender === 'female' ? 'male' : 'female';     
    }
}