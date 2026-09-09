# Food Planner

A Xamarin/MAUI app that used an API to get recipes. 

## Project Structure
The barebone MVVM design:
```text
├── Data/              # Data layer
├── Helpers/           # Helper class Logic
├── Models/            # Entities and definition
├── ViewModels         # Logic layer
└── Views              # Presentation layer
```

## Why I used MVVM?
Testability: Because the ViewModel contains the UI logic without directly referencing UI controls (like buttons or text boxes). Also can easily write automated unit tests for the app's behavior without needing a running graphical interface.

Separation of Concerns: Designers can work on the layout and look of the View, while developers can write the data handling and logic in the ViewModel independently.
I had the Data layer for keeping the API referencing and any data process related, kept separate. I also used a Helper layer to move anything 'custom' in a separate layer.

Maintainability: Changes to the underlying data structure (Model) or layout (View) don't require rewriting the entire codebase, as long as the ViewModel acts as a stable intermediary.
I like layering the design because I can focus on 1 aspect of the system, without breaking things in other places. 
