import 'Link'
export default function AcademicNavbar(props) {
    if (localStorage.getItem('email') == null) {
        return (
            <div>
                <li>
                    <Link to="/">Dogs</Link>
                </li>
                <li>
                    <Link to="/cats">Cats</Link>
                </li>
                <li>
                    <Link to="/sheeps">Sheeps</Link>
                </li>
                <li>
                    <Link to="/goats">Goats</Link>
                </li>
            </div>
        );
    }
    }
    else return (<div>not logged in</div>)
}