import React from 'react'

import DisciplineController from '../../controllers/DisciplineController';
import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import GradeController from '../../controllers/GradeController';
import AuthenticationController from '../../controllers/AuthenticationController';

export default function ViewStudentsPerDiscipline() {
    const params = useParams();
    const [students, setStudents] = useState([]);
    const [newGrade, setNewGrade] = useState(0);
    const [teacher, setTeacher] = useState(0);

    useEffect(() => {
        DisciplineController.getStudentsForCurrentDiscipline(params.id).then((response) => {
            console.log(response);
            setStudents(response);
      }, (error) => {
            console.log("ERROR ", error);
      });
      AuthenticationController.getUser().then((response) => {
        setTeacher(response);
      });

        console.log("params", params.id);
      } ,[]);

    
      function addGrade(student) {
          if (newGrade < 1 || newGrade > 10) {
            alert("Grade should be less than 10 and greater than 0");
            return;
          }
            var grade = {
                studentEmail: student.email,
                teacherEmail: teacher.email,
                mark: newGrade,
                disciplineId: params.id
            }
            
        GradeController.addGrade(grade);
      }

    
    function renderTableData() {
        if(students!= null){
        return students.map((o, index) => { 
            return (
                <tr key={index}>
                    <td>{o.email}</td>
                    <td>{o.firstName}</td>
                    <td>{o.lastName}</td>
                    <td><input type='text' defaultValue={0} onChange={e => setNewGrade(parseInt(e.target.value))}></input></td>
                    <td><button className='btn btn-warning' onClick={() => addGrade(o)}>Add Grade</button></td>
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
                        <th>Email</th>
                        <th>FirstName</th>
                        <th>LastName</th>
                        <th>New Grade</th>
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