### Local development with Docker Compose
## Prerequisites
Before you start, ensure you have the following installed on your machine:
- Linux
- Docker
  
#### WSL 2 linux install (for Windows users):

Open PowerShell or CMD with Administrator rights.

```powershell
# Run the following command to to see a list of available Linux distributions available for download through the online store
wsl.exe --list --online
# Select the distribution you would like to install from the output and install it like:
wsl.exe --install Ubuntu-24.04
```

##### Check official documentation on WSL for troubleshooting: https://learn.microsoft.com/en-us/windows/wsl/install

#### Install Docker:

```bash
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh ./get-docker.sh
```
##### Check official documentation on Docker for troubleshooting: [https://docs.docker.com/engine/install/](https://docs.docker.com/engine/install/ubuntu/#install-using-the-convenience-script)

## Project Structure
The project is organized in the following way:
```
├── compose.yml           # The Docker Compose file
├── frontend
│   ├── angular          # Angular application files
│   └── react            # React application files
├── backend
│   ├── ApiService       # Backend API service files
│   └── Database         # Database setup files
```
## Usage
Create .env file in the same directory as compose.yml file with a following content:

```
PSQL_USER='admin'
PSQL_PASSWORD='' #your password
PSQL_DB='itm'
CONNECTIONSTRINGS__DBCONNECTIONSTRING='Host=db;Port=5432;Database=itm;Username=admin;Password=' #specify your password here
```

Run the following command to build and start all the services defined in the compose.yml file:
```
docker compose up --build
```
The --build flag ensures that Docker builds the images before starting the containers.

## Access the Services
Once the containers are running, you can access the services via the following URLs or use cUrl with -X GET method:

Angular Frontend: http://localhost:8081
React Frontend: http://localhost:8082
Backend API: http://localhost:8080
PostgreSQL Database: Accessible on port 5432 with credentials you specified in ConnectionString (use a database client like psql client, pgAdmin or DBeaver).

## Troubleshooting
If you encounter any issues, here are some common problems and solutions:

If a port (e.g., 80, 8081, 8082 or 5432) is already in use, you can modify the ports section in the compose.yml file to use a different port. For example:
```
ports:
  - "8082:80"  # Change 8081 to 8082
```

Containers Not Starting
- Run the following command to check the logs of a specific container:
```
docker compose logs <service-name>
```

Rebuilding Containers:

- If you did changes to the code and want to rebuild the containers, use:
```
docker compose up --build
```

Removing All Containers and Volumes:
```
docker compose down -v
```
