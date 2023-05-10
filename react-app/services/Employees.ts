import RestUtilities from './RestUtilities';

export interface IAuthenticate {
    email: string,
    password: string;
}

export interface IBase {
    id: number,
    email: string,
    password: string;
}

export interface IEmployeeBase {
    email: string,
    password: string,
    firstName: string,
    lastName: string,
    department: string,
    salary: number;
}

export interface IEmployee {
    id: number,
    email: string,
    password: string,
    firstName: string,
    lastName: string,
    department: string,
    salary: number;
}


export default class  {
    fetchAll(authenticate: IAuthenticate) {
        return RestUtilities.get<Array<IEmployee>>('​/Employee​/RetrieveEmployees');
    }

    fetch(base: IBase) {
        return RestUtilities.get<IEmployee>('/Employee​/RetrieveEmployee}');
    }

    update(employee: IEmployee) {
        return RestUtilities.put<IEmployee>('​/Employee​/UpdateEmployee', employee);
    }

    create(employee: IEmployeeBase) {
        return RestUtilities.post<IEmployee>('​/Employee​/AddEmployee', employee);
    }

    delete(base: IBase) {
        return RestUtilities.delete('/Employee​/RemoveEmployee', base);
    }
}

