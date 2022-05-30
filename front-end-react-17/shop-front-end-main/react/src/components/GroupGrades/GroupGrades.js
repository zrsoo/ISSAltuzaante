import React from 'react'
import "../AproveOptionals/AproveOptionals.css";
import DisciplineController from '../../controllers/DisciplineController';
import { useState, useEffect } from 'react';
import GradeController from '../../controllers/GradeController';
import UserController from "../../controllers/UserController";
import {CSVLink} from "react-csv/lib";

export default function GroupGrades() {
    const [grades, setGrades] = useState([]);
    const [specialization, setSpecialization] = useState();

    const handleSubmit = e => {
        e.preventDefault();
        const data = {
            specializationId: specialization,
        };
        UserController.getStatistics(data.specializationId).then((response) => {
            setGrades(response);
            console.log("grades2",response);
    })
    }

    useEffect(() => {
        console.log("GRADES", grades);
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
                        <td>{o.disciplineId}</td>
                        <td>{o.studentEmail}</td>
                        <td>{o.mark}</td>
                    </tr>
                )
            })
        }

    }

    return (
        <div className='optionals'>
            <form onSubmit={handleSubmit}>
            <div className="form-group">
                <label>Group Id</label>
                <input type="text" className="form-control" placeholder="Group Id"
                       onChange={e => setSpecialization(e.target.value)} />
            </div>
                <button className="btn btn-primary btn-block login-button">Submit</button>
            </form>
            <table className='table table-striped'>
                <thead>
                <tr>
                    <th>DisciplineId</th>
                    <th>Student</th>
                    <th>Grade</th>
                </tr>
                </thead>
                <tbody>
                {renderTableData()}
                </tbody>
            </table>
            {
                grades != undefined && <div><CSVLink data={grades.map(({disciplineId, studentEmail, mark}) => ({disciplineId, studentEmail, mark}))}>Download me</CSVLink></div>
            }
            
            <h2>{}</h2>
        </div>

    );
}