import 'object-assign';
import * as React from 'react';
import { Link, Redirect, RouteComponentProps } from 'react-router-dom';
import EmployeeService, { IEmployee } from '../services/Employees'
import { RoutePaths } from './Routes';

let employeeService = new EmployeeService();

export class EmployeeForm extends React.Component<RouteComponentProps<any>, any> {
    state = {
        employees: null as IEmployee,
        errors: {} as { [key: string]: string }
    }

    componentDidMount() {
        if (this.props.match.path == RoutePaths.EmployeeEdit) {
            employeeService.fetch(this.props.match.params.id).then((response) => {
                this.setState({ employee: response.content });
            });
        } else {
            let newEmployee: IEmployee= {
                lastName: '', firstName: '', department: '', salary: 0
            };
            this.setState({ employee: newEmployee });
        }
    }

    handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        this.saveEmployee(this.state.employee);
    }

    handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;
        let employeeUpdates = {
            [name]: value
        };

        this.setState({
            employee: Object.assign(this.state.employee, employeeUpdates)
        });
    }

    saveEmployee(employee: IEmployee) {
        this.setState({ errors: {} as { [key: string]: string } });
        employeeService.update(employee).then((response) => {
            if (!response.is_error) {
                this.props.history.push(RoutePaths.Employees);
            } else {
                this.setState({ errors: response.error_content });
            }
        });
    }

    _formGroupClass(field: string) {
        var className = "form-group ";
        if (field) {
            className += " has-danger"
        }
        return className;
    }

    render() {
        if (!this.state.employee) {
            return <div>Loading...</div>;
        }
        else {
            return <fieldset className="form-group">
                <legend>{this.state.employee.id ? "Edit Employee" : "New Employee" }</legend>
                <form onSubmit={(e) => this.handleSubmit(e)}>
                    <div className={this._formGroupClass(this.state.errors.lastName)}>
                        <label htmlFor="inputLastName" className="form-control-label">Last Name</label>
                        <input type="text" autoFocus name="lastName" id="inputLastName" value={this.state.employee.lastName} onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" required />
                        <div className="form-control-feedback">{this.state.errors.lastName}</div>
                    </div>
                    <div className={this._formGroupClass(this.state.errors.firstName)}>
                        <label htmlFor="inputFirstName" className="form-control-label">First Name</label>
                        <input type="text" name="firstName" id="inputFirstName" value={this.state.employee.firstName} onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" required />
                        <div className="form-control-feedback">{this.state.errors.firstName}</div>
                    </div>
                    <div className={this._formGroupClass(this.state.errors.department)}>
                        <label htmlFor="inputDepartment" className="form-control-label">Department</label>
                        <input type="email" name="department" id="inputDepartment" value={this.state.employee.department} onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                        <div className="form-control-feedback">{this.state.errors.department}</div>
                    </div>
                    <div className={this._formGroupClass(this.state.errors.salary)}>
                        <label htmlFor="inputSalary" className="form-control-label">Salary</label>
                        <input type="number" name="salary" id="inputSalary" value={this.state.employee.salary} onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" />
                        <div className="form-control-feedback">{this.state.errors.salary}</div>
                    </div>
                    <button className="btn btn-lg btn-primary btn-block" type="submit">Save</button>
                    <Link className="btn btn-lg btn-light btn-block" to="/employees">Cancel</Link>
                </form>
            </fieldset>
        }
    }
}
