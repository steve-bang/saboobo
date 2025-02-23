# Step 1: Use an official Node.js image as the base image
FROM node:22-alpine AS builder

# Step 2: Set the working directory
WORKDIR /app

# Step 3: Install dependencies
##COPY ./package.json ./
COPY ["src/WebApp/package*.json","./"]
RUN npm install


# Step 4: Copy the rest of the application
COPY ["src/WebApp/","./"]

# Step 5: Build the Next.js application
RUN npm run build

# Step 6: Create a new image for the production environment
FROM node:22-alpine AS runner

# Set the working directory for the production image
WORKDIR /app

# Copy only the necessary files for production
COPY --from=builder /app/package.json ./
COPY --from=builder /app/.next .next
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/public ./public

# Expose the port the app will run on
EXPOSE 3000

# Step 7: Command to run the Next.js app in production mode
CMD ["npm", "start"]

# Exec: 
#   Cd root project
#   docker buildx build --platform linux/amd64 -t mrstevebang/saboobo-webapp:1.1.1 -f src/WebApp/Dockerfile .
#   docker push mrstevebang/saboobo-webapp:1.1.1