# syntax=docker/dockerfile:1

# Comments are provided throughout this file to help you get started.
# If you need more help, visit the Dockerfile reference guide at
# https://docs.docker.com/engine/reference/builder/

ARG NODE_VERSION=18.15.0

################################################################################
# Use node image for base image for all stages.
FROM node:${NODE_VERSION}-alpine as base

# Set working directory for all build stages.
WORKDIR .


################################################################################
# Create a stage for installing production dependecies.
FROM base as deps
WORKDIR .
RUN apk add --no-cache libc6-compat


# Download dependencies as a separate step to take advantage of Docker's caching.
# Leverage a cache mount to /root/.npm to speed up subsequent builds.
# Leverage bind mounts to package.json and package-lock.json to avoid having to copy them
# into this layer.
COPY package.json .
COPY package-lock.json .
COPY tsconfig.json .
COPY tailwind.config.ts .
COPY postcss.config.js .

RUN npm ci

################################################################################
# Create a stage for building the application.
FROM deps as build
WORKDIR .
COPY --from=deps /node_modules/ /node_modules/

# Download additional development dependencies before building, as some projects require
# "devDependencies" to be installed to build. If you don't need this, remove this step.
#RUN --mount=type=bind,source=package.json,target=package.json \
#    --mount=type=bind,source=package-lock.json,target=package-lock.json \
#    --mount=type=bind,source=tsconfig.json,target=tsconfig.json \
#    --mount=type=cache,target=/root/.npm \
#    npm ci

# Copy the rest of the source files into the image.
COPY . .
# Run the build script.
RUN npm run build

################################################################################
# Create a new stage to run the application with minimal runtime dependencies
# where the necessary files are copied from the build stage.
FROM base as final

# Use production node environment by default.
ENV NODE_ENV production

RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 nextjs

COPY --from=build /public /public

# Set the correct permission for prerender cache
RUN mkdir .next
RUN chown nextjs:nodejs .next



# Run the application as a non-root user.
USER nextjs

# Copy package.json so that package manager commands can be used.
COPY package.json .
COPY package-lock.json .
COPY tsconfig.json .

# Copy the production dependencies from the deps stage and also
# the built application from the build stage into the image.
COPY --from=build --chown=nextjs:nodejs /node_modules/ /node_modules/
COPY --from=build --chown=nextjs:nodejs /.next/ /.next/
COPY --from=build --chown=nextjs:nodejs /app/ /app/
COPY --from=build --chown=nextjs:nodejs next.config.js .
COPY --from=build --chown=nextjs:nodejs postcss.config.js .
COPY --from=build --chown=nextjs:nodejs tailwind.config.ts .
COPY --from=build --chown=nextjs:nodejs middleware.ts .
COPY --from=build --chown=nextjs:nodejs cypress.config.ts .
COPY --from=build --chown=nextjs:nodejs components components
COPY --from=build --chown=nextjs:nodejs mockData mockData


COPY next-env.d.ts .

# Expose the port that the application listens on.
EXPOSE 3000

ENV NEXT_TELEMETRY_DISABLED 1

# Run the application.
RUN npm run dev
#RUN npm run start
#CMD ["npm", "run", "start"]
