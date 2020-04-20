# Requirements

1. .Net Core 3.1
2. SQL Server

# Setup Environment

1. (We run SQL Server from a Docker image)
    - docker pull mcr.microsoft.com/mssql/server:2017-CU18-ubuntu-16.04
    - docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=p455w0rD!.' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU18-ubuntu-16.04

2. Restore packages
    - dotnet restore

# Build

1. Run migrations
    - dotnet ef database update --startup-project ./src/Sheriff.WebApi/Sheriff.WebApi.csproj --project ./src/Sheriff.Infrastructure/Sheriff.Infrastructure.csproj

2. Build solution
    - dotnet build

# Run and Test

1. Run WebApi
    - dotnet run --project ./src/Sheriff.WebApi/Sheriff.WebApi.csproj;

# Endpoints

1. Band Details
    - Endpoint: GET https://localhost:5001/bands/{bandId}

2. Bands List
    - Endpoint: GET https://localhost:5001/bands[?name={filter}]

3. Bandit Bands List
    - Endpoint: GET https://localhost:5001/bandits/{banditId}/bands

4. Bandit Details
    - Endpoint: GET https://localhost:5001/bandits/{banditId}

5. Bandits List
    - Endpoint: GET https://localhost:5001/bandits

6. Create Band
    - Endpoint: POST https://localhost:5001/bands
    - Payload: {
        "Name": "{Band Name}",
        "Boss": {
            "Id": {bossId}
        }
    }

7. Create Bandit
    - Endpoint: POST https://localhost:5001/bandits
    - Payload: {
        "Name": "{Bandit Name}",
        "Email": "{Bandit Email}"
    }

8. Create Round:
    - Endpoint: POST https://localhost:5001/rounds
    - Payload: {
        "Name": "{Round Name}",
        "Place": "{Round Place",
        "DateTime": "{yyyy-mm-dd}",
        "Sheriff": {
            "Id": {sheriffId}
        },
        "Members": [
            {
                "Id": {memberId}
            },
            {
                "Id": {memberId}
            },
        ],
        "Band": {
            "Id": {bandId}
        }
    }

9. Invite Join Band
    - Endpoint: POST https://localhost:5001/bands/invite
    - Payload: {
        "Host": {
            "Id": {banditId}
        },
        "Guest": {
            "Id": {banditId}
        },
        "Band": {
            "Id": {bandId}
        }
    }

10. Request Join Band
    - Endpoint: POST https://localhost:5001/bands/request
    - Payload: {
        "Band": {
            "Id": {bandId}
        },
        "Guest": {
            "Id": {banditId}
        }
    }

11. Notifications List
    - Endpoint: GET https://localhost:5001/bandits/{banditId}/notifications

12. Read Notification
    - Endpoint: POST https://localhost:5001/notifications/{notificationId}/read

13. Invitations List
    - Endpoint: GET https://localhost:5001/bandits/{banditId}/invites

14. Handle Invitation
    - Endpoint: POST https://localhost:5001/invites/handle
    - Payload: {
        "Accept": {true|false},
        "Invitation": {
            "Id": {invitationId}
        }
    }

15. Invite Join App
    - Endpoint: POST https://localhost:5001/bandits/invite
    - Payload: {
        "Host": {
            "Id": {banditId}
        },
        "GuestEmail": "{Guest Email}"
    }

16. Score Round
    - Endpoint: https://localhost:5001/rounds/score
    - Payload: {
        "Round": {
            "Id": {roundId}
        },
        "Member": {
            "Id": {banditId}
        },
        "Score": {
            "LootSize": {lootSize},
            "LootValue": {lootValue},
            "Service": {servic},
            "Price": {price}
        }
    }