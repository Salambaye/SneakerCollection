
# Sneaker Collection Web Application

**Course:** Retail (e-commerce)  
**Professor:** Prof. Marcel Stefan Wagner, PhD  
**Developer:** Salamata Nourou MBAYE 

## Project Overview

This project is a web application developed using **ASP.NET Core MVC**, **C#**, **Razor Views**, and **Tag Helpers** in **Visual Studio 2022**. The application is inspired by an online market example and allows users to manage a collection of sneakers. The primary objective was to implement **CRUD operations** (Create, Read, Update, Delete) entirely in the browser environment.

**Note:** No database integration is used; data is managed in memory for demonstration purposes.

## Features

- **CRUD Operations:** Users can create, view, update, and delete sneakers.
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

![Home Page](screenshots/home.png)  
_Home page displaying sneaker collection_

![Add Sneaker](screenshots/add_sneaker.png)  
_Add new sneaker form with validations_

![Edit Sneaker](screenshots/edit_sneaker.png)  
_Edit form including image preview and reset button_

![Details](screenshots/details.png)  
_Detailed view of a sneaker with system information_

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

---

**Professor:** Prof. Marcel Stefan Wagner, PhD  
**Developer:** Salamata Nourou MBAYE - ESTIAM - E5 Web And Mobile Development

