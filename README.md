# NorthwindTraders

The **NorthwindTraders Order Management System** is a modern, feature-rich application designed to streamline order processing operations with integrated geolocation services. This system helps employees manage customer orders efficiently with real-time address validation, interactive map visualization, and automated PDF report generation.
Mostrar imagen
## ✨ Key Features
- Comprehensive Order Management

Create, read, update, and delete orders with intuitive forms
Manage customer information, product selection, and shipping details
Assign employees to orders with accountability tracking


- Advanced Address Validation

Google Maps Address Validation API integration
Real-time address standardization and verification
Geocoded coordinates for precise location tracking


- Interactive Map Integration

Embedded Google Maps for visual address confirmation
Interactive location pinning and adjustment
Enhanced spatial awareness for logistics planning


- Professional PDF Reporting

Generate comprehensive order reports with a single click
Customized report templates with company branding
Export detailed order metadata and line items



## 🏗️ Architecture
The NorthwindTraders Order Management System follows CLEAN Architecture principles, with distinct separation of concerns:
```text
NorthwindTraders/
├── NorthwindTraders.Domain/        # Core Domain Logic
│   └── Entities/                   # Domain Entities
│       ├── Order.cs
│       ├── OrderDetail.cs
│       ├── Customer.cs
│       ├── Employee.cs
│       ├── Shipper.cs
│       └── Product.cs
├── NorthwindTraders.Application/   # Application Services
│   ├── DTOs/                       # Data Transfer Objects
│   ├── Exceptions/                 # Custom Exceptions
│   ├── Interfaces/                 # Service Interfaces
│   └── Services/                   # Application Services
├── NorthwindTraders.Infrastructure/ # External Implementations
│   ├── Data/                       # EF Core Implementation
│   │   ├── NorthwindContext.cs     # DbContext
│   │   ├── Repositories/           # Repository Implementations
│   │   └── Migrations/             # EF Core Migrations
│   └── Services/                   # External Services
│       ├── GoogleMapsService.cs    # Google Maps Integration
│       ├── PdfGenService.cs        # PDF Generation Services
│       └── DependencyInjection.cs  # IoC Container Config
├── NorthwindTraders.Blazor/        # Blazor Web Application
│   ├── Pages/                      # Blazor Pages
│   │   ├── Orders/                 # Order Management Pages
│   │   └── Reports/                # Report Pages
│   ├── Shared/                     # Shared Components
│   │   ├── NavMenu.razor
│   │   └── MainLayout.razor
│   └── wwwroot/                    # Static Resources
│       ├── css/
│       ├── js/
│       └── index.html
└── NorthwindTraders.WebApi/        # API Layer
    ├── Controllers/                # API Controllers
    │   └── OrdersController.cs     # Orders API
    └── Program.cs                  # Application Entry Point
```

## 🚀 Getting Started
### Prerequisites

.NET 8 SDK
SQL Server (or compatible database)
Google Maps API Key

### Installation

Clone the repository

bashgit clone https://github.com/your-org/NorthwindTraders.git
cd NorthwindTraders

### Configure API Keys

Create appsettings.Development.json in the NorthwindTraders.WebApi project:
json{
  "ConnectionStrings": {
    "NorthwindDatabase": "Server=localhost;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "GoogleMaps": {
    "ApiKey": "YOUR_GOOGLE_MAPS_API_KEY",
    "AddressValidationEnabled": true
  }
}

### Set up the database

bashcd NorthwindTraders.WebApi
dotnet ef database update

### Run the application

bashdotnet run

### Access the application

Navigate to https://localhost:5001 in your web browser

## 🔌 API Integration
**Google Maps Integration**
The system integrates three key Google Maps APIs:
1. Places Autocomplete API
2. Address Validation API
3. Geocoding API

**PDF Generation**
### 💻 User Interface
The UI follows web accessibility standards and uses a light, professional color scheme with light-green, light-blue, and white tones.
Order Form
Mostrar imagen
The order management form includes:

Customer selection dropdown
Order date picker
Product selection with quantity fields
Real-time calculation of order totals
Address validation with interactive map
Employee assignment

Order List
Mostrar imagen
The orders list view provides:

Sortable and filterable order grid
Quick status indicators
Action buttons for view/edit/delete operations
Export to PDF functionality

## 🔧 Configuration
- Database Configuration
The system uses Entity Framework Core with SQL Server by default. To configure a different database provider:

- Install the appropriate EF Core provider package
Modify the connection string in appsettings.json
Update the DbContext configuration in DependencyInjection.cs

- Google Maps API Configuration
To configure the Google Maps integration:

- Obtain API keys from Google Cloud Console
Enable the following APIs:
    1. Places API
    1. Maps JavaScript API
    1. Geocoding API
    1. Address Validation API


Add your API key to the configuration file

## 🤝 Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

Fork the repository
```
Create your feature branch (git checkout -b feature/amazing-feature)
Commit your changes (git commit -m 'Add some amazing feature')
Push to the branch (git push origin feature/amazing-feature)
Open a Pull Request
```

## 📝 License
This project is licensed under the MIT License - see the LICENSE file for details.