<strong>This app has substitued an SQL Server with Entity Framework Core's In Memory data, to avoid requiring another service for this app. Check the main branch to see code that interacts with an SQL Server database.</strong>

# Contents
- [Introduction](#Introduction)
- [Using the App](#using-the-app)
- [About](#About)

# Introduction 
Business Reporting Software (truly couldn't think of a better name), is a project I created whilst working at Saith Limited. As the name suggests, the primary purpose of the software was to allow people from different daughter companies to create reports on their company finances, share these reports with Seacht to help make business decisions. Such an app required user accounts, CRUD actions for reports, carefully selected resource access management, user management and more.

It was a project I created nearly entirely in my lonesome, with minimal web development experience and having started learning C# only ~6 months before creating the project. I was solely responsible for planning, requirements analysis, design, development. I received some assitance during development and lots during deployment, but everything else was managed by myself.

# Using the App

## App Link
Find the app <a href="https://business-reporting.onrender.com/" target="_blank">here</a>. It should take under a minute to load.
## Creating an Account
You will be met with a login screen with an option to register using an email, name and password. Email structure is enforced, it is recommended to use a small faux email, like "1@1.com" (and copy it the clipboard) as you may need to logout and login if you wish to experience different roles in the app. Unapproved users are unable to access any part of the application that doesn't involve logging in, registering or the home page. This was necessary as the website couldn't be hosted on a local network but the wide web, preventing stray users from accessing the app.

## Approved
All newly registered users are given the following authorization claims: A position of "MD" (Managing Director) and a role of "User". These are the lowest level claims that allow users to create, fill in, save and share their reports as well as delete their own information and account. "MD" users were responsible for creating financial reports about their company which would then be shared for all users with "Seacht" position claims to view (Seacht being the parent company).

### Financial Reports
Financial Reports are the core of the app. These reports are easily created and edited, and are shared once all fields are filled in (a requirement from the primary stakeholder of the app (the Seacht CFO)). Once all fields were filled in, they could be published for all "Seacht" users to see.

## Seacht Access
Providing yourself with the "Seacht" claim would allow you to view any published report. Employees of Seacht would utilize the information as they see fit, also able to make reports to publish for other Seacht persons to see.

## Admin Access
Providing yourself with "Admin" claim allows you to manage other users, available by clicking on 'Your Account' on the top right and then 'Manage Other Users'. Admins can approve newly registered users as well as give users the appropriate rights they should have.


# About
The website is an ASP.NET Core MVC web application containerized with Docker and hosted on Render. The app originally worked with an SQL Server database but to avoid excessive modification this personal version works with EF Core's in memory database.

## Technologies & etc.
<ul>
  <li>ASP.NET (8) Core MVC</li>
  <li>SQL Server & SQL Server Managment Studio</li>
  <li>Entity Framework Core</li>
  <li>Razor Pages</li>
  <li>Bootstrap</li>
  <li>CSS</li>
</ul>

