import React from 'react'
import "../AproveOptionals/AproveOptionals.css";
import DisciplineController from '../../controllers/DisciplineController';
import { useState, useEffect } from 'react';
import GradeController from '../../controllers/GradeController';
import UserController from "../../controllers/UserController";

export default function GrantScholarships() {
    const [grades, setGrades] = useState([]);
    const [specialization, setSpecialization] = useState();

    const handleSubmit = e => {
        e.preventDefault();
        const data = {
            specializationId: specialization,
        };
        UserController.grantScholarships(data.specializationId).then((response) => {
            setGrades(response);
        })
    }

    useEffect(() => {
        renderTableData();
    }, [grades])


    // useEffect(() => {
    //     UserController.getStatistics().then((response) => {
    //         setGrades(response);
    //
    //     }, (error) => {
    //         console.log("ERROR ", error);
    //     });
    // } ,[]);


    function renderTableData() {
        if (grades != null && grades != [] && grades.length > 0) {
            return grades.map((o, index) => {
                return (
                    <tr key={index}>
                        <td>{o.firstName}</td>
                        <td>{o.lastName}</td>
                    </tr>
                )
            })
        }

    }

    return (
        <div className='optionals'>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Year</label>
                    <input type="text" className="form-control" placeholder="Year"
                           onChange={e => setSpecialization(e.target.value)} />
                </div>
                <button className="btn btn-primary btn-block login-button">Submit</button>
            </form>
            <table className='table table-striped'>
                <thead>
                <tr>
                    <th>First name</th>
                    <th>Last name</th>
                </tr>
                </thead>
                <tbody>
                {renderTableData()}
                </tbody>
            </table>
            <h2>{}</h2>
        </div>

    );
}