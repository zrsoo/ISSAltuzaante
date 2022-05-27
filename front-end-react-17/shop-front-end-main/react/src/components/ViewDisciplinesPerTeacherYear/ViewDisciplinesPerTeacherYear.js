import React from 'react'; 
import { useState, useEffect } from 'react';
import DisciplineController from '../../controllers/DisciplineController';
import "../ViewDisciplinesPerTeacherYear/ViewDisciplinesPerTeacherYear.css";
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/js/bootstrap.js';
import Dropdown  from 'react-bootstrap/Dropdown';
import UserController from '../../controllers/UserController';
import { useHistory, useRouteMatch } from 'react-router-dom';

export default function ViewDisciplinesPerTeacherYear() {
    const [optionals, setOptionals] = useState([]);
    const [emails, setEmails] = useState([]);
    const [selectedYear, setSelectedYear] = useState('');
    const [selectedEmail, setSelectedEmail] = useState('');
    const [disciplines, setDisciplines] = useState([]);
    
    const history = useHistory();
    const location = useRouteMatch();

    useEffect(() => {
        DisciplineController.getDisciplinesRankedByAvgGrade().then((response) => {
          setOptionals(response);
        }, (error) => {
          console.log("ERROR ", error);
        });

        UserController.getTeachersEmails().then((response) => {
          setEmails(response);
        }, (error) => {
          console.log("ERROR", error);
        })
      } ,[]);

    function renderTableData() {
      console.log(disciplines);
      console.log(optionals);

      if(disciplines == [] || disciplines.length == 0 || disciplines == null)
      {
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
      else
      {
        return disciplines.map((o, index) => {
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

    const pushSelectedEmailYear = () => {
        if(selectedEmail != "" && selectedYear != "")
          DisciplineController.getDisciplinesByTeacherYear(selectedEmail, selectedYear).then((response) => {
            setDisciplines(response);
          }, (error) => {
            console.log("ERROR ", error);
          });
    }

    function loadEmails() {
      if(emails != null && emails != [] && emails.length > 0)
      {
        return emails.map((email, index) => {
          return (
            <Dropdown.Item eventKey={email.email} key={index} href={'#'}>{email.email}</Dropdown.Item>
          )
        })
      }
    }
     
    const handleSelectEmail = (e) => {
      console.log(e);

      setSelectedEmail(e);
    }

    const handleSelectYear = (e) => {
      console.log(e);

      setSelectedYear(e);
    }

    return (
      <div className="contentWrapper">
        <div className="dropdownContainer">
          <p>If your search terms return nothing, the table will go back to printing all disciplines.</p>
          <Dropdown onSelect={handleSelectEmail} className='teacherDropdown'>
            <Dropdown.Toggle variant="success">Teacher email</Dropdown.Toggle>
            <Dropdown.Menu >
              {loadEmails()}
            </Dropdown.Menu>
          </Dropdown>

          <Dropdown onSelect={handleSelectYear} className='yearDropdown'>
            <Dropdown.Toggle variant="success">Year</Dropdown.Toggle>
            <Dropdown.Menu>
              <Dropdown.Item eventKey='1' href="#">1</Dropdown.Item>
              <Dropdown.Item eventKey='2' href="#">2</Dropdown.Item>
              <Dropdown.Item eventKey='3' href="#">3</Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>

          <button onClick={pushSelectedEmailYear} className='btn-primary btn search-button'>Search</button>
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