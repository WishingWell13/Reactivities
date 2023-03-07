import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';
import { Header } from 'semantic-ui-react';
import List from 'semantic-ui-react/dist/commonjs/elements/List';

function App() {
  const [activities, setActivities] = useState([]);

  //axios.get() returns a promise. Chaining a then method determines what to 
  //do with promise
  //if we get the promise, use the response to setActivities
  useEffect(() => {
    axios.get('http://localhost:5000/api/activities')
    .then(response => {
		console.log(response);
		//Activities are held in data object. Update activities variable
		setActivities(response.data);
    }); 
  }, [
	//This is an empty dependency array to prevent infinite looping
	//Would infinitely loop otherwise b/c after you update activities, component
	//is updated, so would call useEffect again
  ])

  return (
    <div>
		<Header as='h2' icon='users' content='Reactivities' />
        <List>
			{activities.map((activity: any) => (
				<li key={activity.id}>{activity.title}</li>	
			))}
		</List>

    </div>
  );
}

export default App;
