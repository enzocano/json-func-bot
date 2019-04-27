const codename = require('project-name-generator');
const restify = require('restify');
const request = require('request');
const uuid = require('uuid/v4');

const secret = process.env.SECRET;

function generateName() {
    return codename({
        words: 3,
        number: false
    }).spaced;
};

const server = restify.createServer();

server.use(restify.plugins.queryParser());

server.get('/', restify.plugins.serveStatic({
    directory: __dirname,
    default: 'index.html'
}));

server.get('/config', (req, res, next) => {
    const userId = req.query.id || uuid();
    const userName = req.query.name || generateName();
    const options = {
        method: 'POST',
        uri: 'https://directline.botframework.com/v3/directline/tokens/generate',
        headers: {
            'Authorization': 'Bearer ' + secret
        },
        json: {
            User: { Id: userId }
        }
    }

    request.post(options, (error, response, body) => {
        if (!error && response.statusCode < 300) {
            res.json({
                token: body.token,
                id: userId,
                name: userName
            });
        } else {
            res.send(500, 'Call to retrieve token from Direct Line failed');
        }
    });
});

server.listen(process.env.port || process.env.PORT || 8080);
