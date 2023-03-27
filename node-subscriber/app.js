//
// Copyright 2021 The Dapr Authors
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

const express = require('express');
const bodyParser = require('body-parser');
const lzbase62 = require('lzbase62');
const app = express();
// Dapr publishes messages with the application/cloudevents+json content-type
app.use(bodyParser.json({ type: 'application/*+json' }));

var uno="";
var dos="";
const port = 3000;

app.get('/dapr/subscribe', (_req, res) => {
    res.json([
        {
            pubsubname: "pubsub",
            topic: "A",
            route: "A"
        },
        {
            pubsubname: "pubsub",
            topic: "B",
            route: "B"
        },
        {
            pubsubname: "pubsub",
            topic: "C",
            route: "C"
        }
    ]);
});

app.post('/A', (req, res) => {
    console.log("A: ", req.body.data.message);
    
console.log(req.body.data.message);

uno=req.body.data.message;
    res.sendStatus(200);
});

app.post('/B', (req,res ) => {
    console.log("B: ", req.body.data.message);
    res.sendStatus(200);
    
    var compressed = lzbase62.compress(uno);
    console.log(compressed);
    dos=compressed; // 'hello hello hello'
});
app.post('/C', (req, res) => {
    console.log("C: ", dos);
    res.sendStatus(200);
    var decompressed = lzbase62.decompress(dos);
    console.log(decompressed); // 'hello hello hello'
 // true
});

app.listen(port, () => console.log(`Node App listening on port ${port}!`));
