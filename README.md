
# Sneaker Collection Web Application

**Course:** Retail (e-commerce)  
**Professor:** Prof. Marcel Stefan Wagner, PhD  
**Developer:** Salamata Nourou MBAYE 

## Project Overview

This project is a web application developed using **ASP.NET Core MVC**, **C#**, **Razor Views**, and **Tag Helpers** in **Visual Studio 2022**. The application is inspired by an online market example and allows users to manage a collection of sneakers. The primary objective was to implement **CRUD operations** (Create, Read, Update, Delete) entirely in the browser environment.

**Note:** No database integration is used; data is managed in memory for demonstration purposes.

## Features

- **CRUD Operations:** Users can create, view, update, and delete sneakers.
  - **Create**: Add a new sneaker with all its properties (brand, model, size, colorway, price, etc.).
  - **Read**: View sneaker details in a dedicated page, with a clean Bootstrap layout and image preview.
  - **Update**: Edit an existing sneaker’s information via a structured form (with enums, dates, and validations).
  - **Delete**: Remove a sneaker from the collection.  
     - The system always asks for confirmation before deletion, preventing accidental removals.  
     - Once confirmed, the sneaker is permanently removed from the in-memory list.
       
- **Search Functionality:** Users can search sneakers by brand, model, or category.
- **Data Types Used:** Various data types are handled, including:
  - `String` (e.g., Model, Colorway)
  - `Enum` (e.g., Brand, Category, Condition)
  - `Date` (Release Date)
  - `Boolean` (Limited Edition)
- **Form Validation:** Implemented using Data Annotations.
- **Tag Helpers:** Custom TagHelpers are created to simplify repetitive HTML elements.
- **Responsive Design:** The layout uses **Bootstrap 5** and **Bootswatch** templates.
- **Confirmation Prompts:** Deleting an item requires user confirmation to prevent accidental removal.
- **Image Previews:** Sneaker images can be previewed dynamically before saving.
- **User Experience Enhancements:** Includes features like reset to original form, dynamic description character counter, and confirmation dialogs.

## Screenshots

<img width="1212" height="865" alt="Home" src="https://github.com/user-attachments/assets/c2641da3-028c-4aa5-ba81-bc185c30ca04" />
<img width="1026" height="881" alt="Home2" src="https://github.com/user-attachments/assets/80c79fef-4e64-4db1-837c-7b32190370e4" />

_Home page displaying sneaker collection_

![Add Sneaker](screenshots/add_sneaker.png)  
_Add new sneaker form with validations_

  <img width="860" height="813" alt="Update1" src="https://github.com/user-attachments/assets/c867d660-9f9c-4d00-9ecc-386efd19cdfd" />
<img width="641" height="858" alt="Update2" src="https://github.com/user-attachments/assets/83585ec0-d894-4847-adcc-56b73793ae79" />
<img width="1492" height="817" alt="Update3" src="https://github.com/user-attachments/assets/caf8c1f8-6321-45d5-ba94-bc2a5ca885f1" />

_Edit form including image preview and reset button_

 <img width="1367" height="887" alt="Details" src="https://github.com/user-attachments/assets/71dc7ac2-7e09-49db-9918-5c345131195d" />

_Detailed view of a sneaker with system information_

<img width="1307" height="857" alt="Search1" src="https://github.com/user-attachments/assets/b9cc1d7d-ad6e-46ca-aa30-67c1c7575bfa" />
<img width="1303" height="823" alt="Search2" src="https://github.com/user-attachments/assets/91063fb3-fd00-4ba8-8757-4f1923ffc693" />
_Research of a sneaker _


## Project Structure

- **Controllers**: Manage HTTP requests and CRUD operations.
- **Models**: Define Sneaker model with properties, enums, and data annotations.
- **Views**: Razor views for Create, Edit, Details, Index pages.
- **TagHelpers**: Custom reusable Tag Helpers for UI components.
- **wwwroot**: Contains static assets (CSS, JS, images).
- **Scripts**: JavaScript code for dynamic behavior (image preview, form reset, etc.).

## Installation and Usage

1. Open the project in **Visual Studio 2022**.
2. Build the solution.
3. Run the project in the browser (F5 or Ctrl+F5).
4. Interact with the Sneaker Collection application using CRUD operations.

## License

This project is for educational purposes and course submission. All rights reserved by the developer.


**Professor:** Prof. Marcel Stefan Wagner, PhD  
**Developer:** Salamata Nourou MBAYE - ESTIAM - E5 Web And Mobile Development

