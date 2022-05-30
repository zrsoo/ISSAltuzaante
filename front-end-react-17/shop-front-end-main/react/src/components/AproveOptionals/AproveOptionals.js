import React from 'react'
import "../AproveOptionals/AproveOptionals.css";
import DisciplineController from '../../controllers/DisciplineController';
import { useState, useEffect } from 'react';
import UserController from '../../controllers/UserController';

export default function AproveOptionals() {
    const [optionals, setOptionals] = useState([]);
    const [newMaximum, setNewMaximum] = useState();

    useEffect(() => {
      DisciplineController.getOptionalDisciplines().then((response) => {
        setOptionals(response);
      
    }, (error) => {
        console.log("ERROR ", error);
    });
    } ,[]);

    function updateOptional(discipline) {
        if (newMaximum != null) {
            const newDiscipline = {
                disciplineId: discipline.disciplineId, 
                name: discipline.name,
                isOptional: discipline.isOptional, 
                numberOfStudents: discipline.numberOfStudents, 
                maxNumberOfStudents: newMaximum,
                facultyId: discipline.facultyId,
                year: discipline.year
            }

            DisciplineController.updateDiscipline(newDiscipline);
        }
        setNewMaximum(null);
    }

    function renderTableData() {
        if (optionals != null && optionals != [] && optionals.length > 0) {
            return optionals.map((o, index) => {
                return (
                    <tr key={index}>
                        <td>{o.name}</td>
                        <td>{o.isOptional}</td>
                        <td>{o.facultyId}</td>
                        <td>{o.numberOfStudents}</td>
                        <td><input type='text' id={'optional ' + o.disciplineId} defaultValue={o.maxNumberOfStudents} onChange={e => setNewMaximum(parseInt(e.target.value))}></input></td>
                        <td>{o.year}</td>
                        <td><button className='btn btn-info' onClick={() => updateOptional(o)}>Update</button></td>
                    </tr>
                )
            })
        }
        
    }

    function approveOptionals() {
        UserController.approveOptionals();
    }

    return (
        <div className='optionals'>
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>IsOptional</th>
                        <th>FacultyId</th>
                        <th>Number of students</th>
                        <th>Maximum number of students</th>
                        <th>Year</th>
                    </tr>
                </thead>
                <tbody>
                    {renderTableData()}
                </tbody>
            </table>
            <td><button className='btn btn-success' onClick={() => approveOptionals()}>Approve Optionals</button></td>
        </div>

    );
}