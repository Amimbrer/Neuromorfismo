# Neuromorfismo

That project started at a big challenge for me, it are going to be a real private web app that some doctors of Spain would use for a study about a special disease in childrens
I started that project using C# and .NET 7 without any experience in C# and Blazor. I was for 2 months as a self-taught learning about the best practice of C# and Blazor for start that project alone.

Actually, at 08/12/2023, three months ago without 100% of the project completed, I can say I am so proud of the decision of started that project being alone because I was and continue learning a lots of things about C# and Blazor in internet, whatchign a lots of videos, blogs, and a lot of effort.

I did not receive any money for this project, on the contrary, I gladly accepted to do it for free. I only wanted to contribute more experience in .NET for companies to see my potencial and my willingness to learn.

## Let's go to the importat, what does the application consist of?
  > The App allow to the doctors register patients with a lots of fields, and later of the registration they can start with a tracking about that patient. In this tracking the fill out a form a timeline
  > with the progression of the patient. In the timeline the had to specify some importants thing for the study, I am not going to go into details about the fields because is boring and not important...
  > The doctos can also edit fields (of the register) of his own patients, they can filter and edit the current point of the timeline of a patient if he didn't confirm it yet.

But it is not all, because now enter the most important part of the App. The App separate for three different types of users:
1. Super Admin.
2. Admin.
3. Doctor.

The permisions of each type are decreasing in the list... 
1. Super Admin: this users can do all the things that the rest of the users can do. They are the gods of the App...
  - They can register new users for the App, both admins and doctors, but no a other super admins...
  - They can too visualice a table with all the users of the aplication without others superadmins, they can edit fields of that users.
  - Of couse they can't see password or other confindencial things about users.
2. Admins: this users can also create other users, see and edit, but in his case only doctors users.
  - They can create, visualize, edit and delete some fields which later will appear as selectable list when the doctos create a new patient.
  - They can visualize all the patients of the App, filter, edit and delete patients.
  - They can visualize also all the advance of the timeline of all patients, but dont edit it.
3. Doctors: they only can create patients and visualize, edit and delete the patients assigned to him.

  > The Admins will be able asign more than 1 doctor to a patient, and all will can edit that patient.

### Submit of emails and create excels
When a doctor advance in the timeline of one of his patients and he confirm the advantage all the Super Admins and Admins receive an email with the information of the advance, and the new actual state of the timeline of that patient.
The Super Admins and Admins, who are the only that can watch all the patients, also have the option to generate a excel with all the actual information of all the patients...

And that are all the funcionalities of my app, now I am going to explain what tecnologies I used for it.

## Tecnologies used
1. Backend: for the backend I created a REST API to allow all the connection between the frontEnd, business logic, and conecction with DataBase
    - REST API
    - C#
    - Identity (for security and filter the permits between the three types of users)
    - Entity Framework
    - Automapper
    - N-layer structure
      1. API APP
      2. Services/Business layer
      3. ServicesDependencies/Interfaces layer
      4. Dtos layer
      5. Models for database layer
    - Dependency injection
    - Abstraction with interfaces
    - Linq
    - Async and await structure

2. FrontEnd: for the frontEnd I decided use Blazor WebAssembly and some javascript (it was the only frontEnd lenguage than I controlled a lots)
    - Blazor WebAssembly App.
    - N-layer structure.
      1. Services/Business layer, to all the logic and fetch to the API.
      2. ServicesDependencies/Interfaces layer.
      3. Dtos layer.
    - MudBlazor for the UI of the app.
    - Dependency injection.
    - Abstraction with interfaces.
    - Organized structure in components to try reutilice code, using parameters, cascading values.
    - I used .razor, .razor.cs and .razor.css files for the structure of almost all the components.
    - Linq
    - Async and await structure

3. Shared: that folder is shared between the BackEnd and FrontEnd and is used mainly to reutilice the Dtos between de two apps
   - N-layer structure.
    1. Dtos layer.
    2. Services layer with some small functions of validation for de dtos.
  
I tryed to do the best code in all my project, using the better techniques, CLEAN CODE, SOLID principles...

## Some pictures of the App: 
![WebMedicina0](https://github.com/Amimbrer/WebMedicina/assets/136268910/b5553272-74a2-47ba-b2b3-9e0df8b02d2c)
![WebMedicina1](https://github.com/Amimbrer/WebMedicina/assets/136268910/28665e8f-a320-4c12-9cb3-748bbb613469)
![WebMedicina2](https://github.com/Amimbrer/WebMedicina/assets/136268910/e9ea9c2d-be80-4ca6-a7a3-c5c2d8a932b9)
![WebMedicina3](https://github.com/Amimbrer/WebMedicina/assets/136268910/ef966251-80a0-4630-9d01-ee9da68d407d)
![WebMedicina4](https://github.com/Amimbrer/WebMedicina/assets/136268910/5d74dab7-5460-44cd-989e-6cdfd0e9cdf3)
