import React from 'react';
import { useState, useEffect } from 'react';
import DisciplineController from '../../controllers/DisciplineController';
import "../ViewDisciplinesPerTeacherYear/ViewDisciplinesPerTeacherYear.css";

export default function ViewDisciplinesPerTeacherYear() {
    const [optionals, setOptionals] = useState([]);

    useEffect(() => {
        DisciplineController.getDisciplinesRankedByAvgGrade().then((response) => {
          setOptionals(response);
      }, (error) => {
          console.log("ERROR ", error);
      });
      } ,[]);

    function renderTableData() {
        if (optionals != null && optionals != [] && optionals.length > 0) {
            return optionals.map((o, index) => {
                return (
                    <tr key={index}>
                        <td>{o.name}</td>
                        <td>{o.isOptional.toString()}</td>
                        <td>{o.facultyId}</td>
                        <td>{o.numberOfStudents}</td>
                        <td>{o.maxNumberOfStudents}</td>
                        <td>{o.year}</td>
                        <td>{o.teacherEmail}</td>
                        <td>{o.averageGrade}</td>
                    </tr>
                )
            })
        }
    }

    return (
        <div className="contentWrapper">
            <div className="discipline-table-wrapper">
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>IsOptional</th>
                            <th>FacultyId</th>
                            <th>Number of students</th>
                            <th>Maximum number of students</th>
                            <th>Year</th>
                            <th>Teacher Email</th>
                            <th>Average grade</th>
                        </tr>
                    </thead>
                    <tbody>
                        {renderTableData()}
                    </tbody>
                </table>
            </div>
        </div>
    );
}