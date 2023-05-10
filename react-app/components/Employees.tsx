import * as React from "react";
import { Link, Redirect } from 'react-router-dom';
import { RoutePaths } from './Routes';
import { EmployeeForm } from './EmployeeForm';
import EmployeeService, { IEmployee } from '../services/Employees';
import { RouteComponentProps } from "react-router";

let EmployeeService = new EmployeeService();

export class Employees extends React.Component<RouteComponentProps<any>, any> {
    refs: {
        query: HTMLInputElement;
    };

    state = {
        employees: [] as Array<IEmployee>,
        editEmployee: null as Object,
        isAddMode: false as boolean
    };

    componentDidMount() {
        this.showAll();
    }

    showAll() {
        employeeService.fetchAll().then((response) => {
            this.setState({ employees: response.content });
        });
    }

    delete(employee: IEmployee) {
        employeeService.delete(employee.id).then((response) => {
            let updatedEmployees = this.state.employees;
            updatedEmployees.splice(updatedEmployees.indexOf(employee), 1);
            this.setState({ employees: updatedEmployees });
        });
    }

    render() {
        return <div>
            <h1>Employees</h1>
            {this.state.employees && this.state.employees.length > 0 &&
                <table className="table">
                    <thead>
                        <tr>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Department</th>
                            <th>Salary</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.employees.map((employee, index) =>
                            <tr key={employee.id}>
                                <td>{employee.firstName}</td>
                                <td>{employee.lastName}</td>
                                <td>{employee.department}</td>
                                <td>{conemployeetact.salary}</td>
                                <td><Link to={RoutePaths.EmployeeEdit.replace(":id", employee.id.toString())}>edit</Link>
                                    <button type="button" className="btn btn-link" onClick={(e) => this.delete(employee)}>delete</button></td>
                            </tr>
                        )}
                    </tbody>
                </table>
            }
            <Link className="btn btn-success" to={RoutePaths.EmployeeNew}>add</Link>

        </div>
    };
}
