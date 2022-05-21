import React from 'react'; 
import { useState, useEffect } from 'react';
import DisciplineController from '../../controllers/DisciplineController';
import "../ViewDisciplinesPerTeacherYear/ViewDisciplinesPerTeacherYear.css";
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/js/bootstrap.js';
import Dropdown  from 'react-bootstrap/Dropdown';
import DropdownItem from 'react-bootstrap/esm/DropdownItem';

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

    //  TODO:    1.) Make a function similar to renderTableData that loads Drowpdown.Items in dropdowns (containing all teacher emails, it's ok to hardcode years)
    //           2.) For each click on an option, perform GET request (most comfortably with query parameters in link) that gets disciplines for specified teacher and year
    //           3.) Once a teacher or a year is selected, set the Dropdown.Toggle to display the selected item.
     
    return (
      <div className="contentWrapper">
        <div className="dropdownContainer">
          <Dropdown className='teacherDropdown'>
            <Dropdown.Toggle variant="success">Teacher email</Dropdown.Toggle>
            <Dropdown.Menu>
              <Dropdown.Item href="#">Home Page</Dropdown.Item>
              <Dropdown.Item href="#">Settings</Dropdown.Item>
              <Dropdown.Item href="#">Logout</Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>

          <Dropdown className='yearDropdown'>
            <Dropdown.Toggle variant="success">Year</Dropdown.Toggle>
            <Dropdown.Menu>
              <Dropdown.Item href="#">Home Page</Dropdown.Item>
              <Dropdown.Item href="#">Settings</Dropdown.Item>
              <Dropdown.Item href="#">Logout</Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>
        </div>
        <div className="discipline-table-wrapper">
          <table className="table table-striped">
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
            <tbody>{renderTableData()}</tbody>
          </table>
        </div>
      </div>
    );
}