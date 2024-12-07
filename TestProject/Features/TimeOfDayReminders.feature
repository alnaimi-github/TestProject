@Automations
Feature: Dishwasher reminders
	These reminders will be triggered when there is motion
	in the kitchen. There are several scenarios based
	on the state of the dishwasher, time of day and elapsed
	time between reminders.

Background:
	Given the dishwasher is complete
	And I have not acknowledged unloading it

Scenario: dont promt again that dishwasher is done if last prompt was within the last 60 minutes
	Given it is after 7 AM
	When there is motion in the kitchen
	Then my smart speaker announces it's done and prompts me if it has been unloaded and stores the acknowledgement

Scenario: promt on motion based on room and time of day
	Given the following config
		| roomName | TimeOfDay           |
		| kitchen  | 2023-04-06 06:59:59 |
		| kitchen  | 2023-04-06 07:00:01 |
		| laundry  | 2023-04-06 08:00:01 |
		| bedroom  | 2023-04-06 21:00:01 |
		| bedroom  | 2023-04-06 23:55:21 |
	When there is motion in room at specified time
	Then my smart speaker prompted
		| SpeakerName     | Message                                   | Sent                |
		| speaker.kitchen | Dishwasher is done, have you unloaded it? | 2023-04-06 07:00:01 |
	And my smart speaker announced
		| SpeakerName     | Message                | Sent                |
		| speaker.laundry | Dryer is ready         | 2023-04-06 08:00:01 |
		| speaker.bedroom | Remember to set alarms | 2023-04-06 21:00:01 |

Scenario Outline: no announcements during quiet time
	Given it is before 5 AM
	When there is motion in the <roomName>
	Then there is no announcement

Examples:
	| roomName |
	| kitchen  |
	| laundry  |
	| bedroom  |