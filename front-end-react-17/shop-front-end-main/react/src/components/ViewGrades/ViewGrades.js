import React from 'react'
import "../AproveOptionals/AproveOptionals.css";
import DisciplineController from '../../controllers/DisciplineController';
import { useState, useEffect } from 'react';
import GradeController from '../../controllers/GradeController';
import UserController from "../../controllers/UserController";

export default function ViewGrades() {
    const [grades, setGrades] = useState([]);

    useEffect(() => {
      GradeController.getGrades().then((response) => {
        setGrades(response);

    }, (error) => {
        console.log("ERROR ", error);
    });
    } ,[]);




    function renderTableData() {
        if (grades != null && grades != [] && grades.length > 0) {
            return grades.map((o, index) => {
                return (
                    <tr key={index}>
                        <td>{o.disciplineId}</td>
                        <td>{o.teacherEmail}</td>
                        <td>{o.mark}</td>
                    </tr>
                )
            })
        }
        
    }

    return (
        <div className='optionals'>
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>DisciplineId</th>
                        <th>Teacher</th>
                        <th>Grade</th>
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