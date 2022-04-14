Based on tutorial
https://www.youtube.com/watch?v=ZXdFisA_hOY&t=84s

# statement to setup mongo db with docker

docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo

# add new volume that requires authentication for container

docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=Pass#word1 mongo

# init .net secrets manager for project

dotnet user-secrets init

## add secret

dotnet user-secrets set MongoDbSettings:Password Pass#word1

# health checks repo

https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
