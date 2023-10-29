# weather-station-api

## Database setup
A Docker SQL image has been created based on Azure SQL Edge image.

Tables `WeatherStations`, `Variables` and `Data` has been created using:

```
CREATE TABLE WeatherStations(
Id INT IDENTITY(1,1),
Name NVARCHAR(255),
Site NVARCHAR(255),
Portfolio NVARCHAR(255),
State NVARCHAR(255)
)

TODO: sql for adding other tables
```

The data from WeatherStations.csv has been imported to the database using:

```
BULK INSERT WeatherStations
FROM '/weather_stations.csv'
WITH
(
    FORMAT = 'CSV', 
    FIRSTROW = 2,
    ROWTERMINATOR = '0x0a',
    FIELDTERMINATOR = ','
)

TODO: sql for seeding other tables
```

In the above, the `FROM` was important. The csv file needed to be in the SQL container created. Therefore the file had to be copied over from the hard drive to the container using:
`docker cp weather_stations.csv 78db850d4c06:/weather_stations.csv`

`78db850d4c06` being the container ID.

The change was committed to the image and a new tag was created. Then this image was pushed to Docker Hub.
This image now can be used as the database when running the app.


