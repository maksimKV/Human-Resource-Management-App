import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Route, Redirect, Switch } from 'react-router-dom';
import { ErrorPage } from './Error';
import { Employees } from './Employees';
import { EmployeeForm } from './EmployeeForm';
import { Header } from './Header';

export class RoutePaths {
    public static Employees: string = "/employees";
    public static EmployeeEdit: string = "/employee/edit/:id";
    public static EmployeeNew: string = "/employee/new";
}

export default class Routes extends React.Component<any, any> {
    render() {
        return <Switch>
            <DefaultLayout exact path={RoutePaths.Employees} component={Employees} />
            <DefaultLayout path={RoutePaths.EmployeeNew} component={EmployeeNew} />
            <DefaultLayout path={RoutePaths.EmployeeEdit} component={EmployeeEdit} />
            <Route path='/error/:code?' component={ErrorPage} />
        </Switch>
    }
}

const DefaultLayout = ({ component: Component, ...rest }: { component: any, path: string, exact?: boolean }) => (
    <Route {...rest} render={props => (
        AuthService.isSignedIn() ? (
            <div>
                <Header {...props} />
                <div className="container">
                    <Component {...props} />
                </div>
            </div>
        ) : (
                <Redirect to={{
                    pathname: RoutePaths.SignIn,
                    state: { from: props.location }
                }} />
            )
    )} />
);
