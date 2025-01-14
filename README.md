# RTTClientManagementApp# RTT Client Management Application

## Overview

The RTT Client Management Application is designed to capture and maintain client information, including multiple addresses and contact information. It supports exporting client data to a CSV file, making it easy to manage and share client details.

## Features

- Capture client name, gender, and basic details.
- Manage multiple addresses and contact information for each client.
- Export client and address data to a CSV file (excluding contact numbers).
- Built with a 3-tier architecture for scalability and maintainability.
- Asynchronous operations for improved performance.
- Robust error handling and logging using NLog.

## Architecture

The application follows a 3-tier architecture:

1. **Presentation Layer**: User interface for interacting with the application.
2. **Business Logic Layer**: Handles validation and business rules.
3. **Data Access Layer**: Manages database interactions through a WCF service.

## Setup

### Prerequisites

- .NET Framework (version compatible with your environment)
- SQL Server (or another supported database)
- Visual Studio (for development and testing)

### Installation

1. **Clone the Repository**

   ```shell
   git clone https://github.com/RTT/ClientManagementApp.git