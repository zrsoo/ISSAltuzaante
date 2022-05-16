import React from 'react'

import DisciplineController from '../../controllers/DisciplineController';
import { useState, useEffect } from 'react';

export default function ViewOptionals() {
    const [optionals, setOptionals] = useState([]);
    const [preferences, setPreferences] = useState([{optionalId:-1, preference:-1}]);
    const [opPref, saveOpPref] = useState(-1);


    useEffect(() => {
        DisciplineController.getOptionals().then((response) => {
            console.log(response);
            setOptionals(response);
      }, (error) => {
            console.log("ERROR ", error);
      });
      } ,[]);
    

    function renderTableData() {
        if(optionals!= null){
        return optionals.map((o, index) => { 
            return (
                <tr key={index}>
                    <td>{o.name}</td>
                    <td>{o.isOptional}</td>
                    <td>{o.facultyId}</td>
                    <td>{o.numberOfStudents}</td>
                    <td>{o.maxNumberOfStudents}</td>
                    <td><input type="text" onChange={e => saveOpPref(e.target.value) }></input></td>
                    <td><button className='btn btn-info' onClick={() => {const dict = preferences.concat({ optionalId: o.disciplineId, preference: opPref }); setPreferences(dict); console.log(preferences);}}>Set Preference</button></td>
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
                        <th>Preference</th>
                    </tr>
                </thead>
                <tbody>
                  {renderTableData()}
                  
                </tbody>
               
            </table>

          <h2>{}</h2>
          <button className='btn btn-info' onClick={() => {assignOptional()}}>Save</button>
        </div>

    );

    function assignOptional(){
        DisciplineController.assignOptionalForStudent(preferences);
        setPreferences({optionalId:-1, preference:-1});
    };
}