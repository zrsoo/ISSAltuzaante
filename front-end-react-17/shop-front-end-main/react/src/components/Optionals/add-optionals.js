import React from 'react'
import "../Optionals/addOptionals.css"
import { useState, useEffect } from 'react'
import DisciplineController from '../../controllers/DisciplineController';

export default function AddOptionals(props){
    const [optionals, setOptionals] = useState([]);

    useEffect(() => {
        DisciplineController.getOptionalDisciplines().then((response) => {
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
                        <td>{o.isOptional}</td>
                        <td>{o.facultyId}</td>
                        <td>{o.numberOfStudents}</td>
                        <td>{o.maxNumberOfStudents}</td>
                        <td>{o.year}</td>
                    </tr>
                )
            })
        }
    }

    return (
        <div className="optionals-wrapper">
            <div className='optionals-info-wrapper'>
                <h1>Hello.</h1>
                <h2>You have added x optionals. You can add y more.</h2>
            </div>

            <div className="optionals-form-wrapper">
                <form class="interactionForm">
                    <fieldset>
                        <legend class="optionals-legend">Add optional</legend>
                        <label class="label" for="txtName">Name:</label>
                        <input class="interactionTextInput" type="text" id="txtName"></input>
                        <label class="label" for="txtFacultyId">FacultyId:</label>
                        <input class="interactionTextInput" type="text" id="txtFacultyId"></input>
                        <label class="label" for="txtNumberStudents">Number Of Students:</label>
                        <input class="interactionTextInput" type="text" id="txtNumberStudents"></input>
                        <label class="label" for="txtMaxNumberStudents">Maximum Number of Students:</label>
                        <input class="interactionTextInput" type="text" id="txtMaxNumberStudents"></input>
                        <label class="label" for="txtYear">Year:</label>
                        <input class="interactionTextInput" type="text" id="txtYear"></input>
                        <input class="submitButton" id="updateButton" type="button" value="Add optional"></input>
                    </fieldset>
                </form>
            </div>

            <div className="optionals-table-wrapper">
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
            </div>
        </div>   
    );
}