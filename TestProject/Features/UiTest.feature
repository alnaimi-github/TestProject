@UI
Feature: UI Login

Scenario: Login to Home Assistant with valid credentials
	Given the user is on login page
	When the user logs in with valid credentials
	Then the home automation dashboard is visible

Scenario: Login to Home Assistant with invalid credentials
	Given the user is on login page
	When the user logs in with invalid credentials
	Then the home automation system throws an exception