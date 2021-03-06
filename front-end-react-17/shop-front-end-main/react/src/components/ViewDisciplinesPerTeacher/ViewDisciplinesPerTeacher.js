import React from 'react'

import DisciplineController from '../../controllers/DisciplineController';
import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

export default function ViewDisciplinesPerTeacher() {
    const [disciplines, setDisciplines] = useState([]);
    

    useEffect(() => {
        DisciplineController.getTeacherDisciplines().then((response) => {
            console.log(response);
            setDisciplines(response);
      }, (error) => {
            console.log("ERROR ", error);
      });
      } ,[]);

    
    function renderTableData() {
        if(disciplines!= null){
        return disciplines.map((o, index) => { 
            return (
                <tr key={index}>
                    <td>{o.name}</td>
                    <td>{o.isOptional}</td>
                    <td>{o.facultyId}</td>
                    <td>{o.numberOfStudents}</td>
                    <td>{o.maxNumberOfStudents}</td>
                    <td>{o.year}</td>
                    <td>{o.teacherEmail}</td>
                    <td><Link to={`/discipline/${o.disciplineId}/students`} className="btn btn-info">View enrolled students</Link></td>
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
                        <th>Name</th>
                        <th>IsOptional</th>
                        <th>FacultyId</th>
                        <th>Number of students</th>
                        <th>Maximum number of students</th>
                        <th>Year</th>
                        <th>Email</th>
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