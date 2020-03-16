FROM node:latest

RUN mkdir -p ~/app
WORKDIR app
COPY package.json .
COPY public ./public
COPY src ./src

EXPOSE 3000

RUN npm install --silent

CMD ["npm", "start"]