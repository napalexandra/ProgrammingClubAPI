Feature: Retrive Member By ID 

Scenerio: Existing Member
	Given a member exists with IdMember "5d24bbe4-3d32-4d0c-98a9-0da2b984904c"
	When the member is requested by IdMember
	Then the response should contain the member with the same IdMember