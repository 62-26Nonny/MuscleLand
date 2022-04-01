const express = require('express');
const app = express();
const cors = require('cors');

//app.use(cors());
app.use(express.json());

let data;

let db = new sqlite3.Database('./data/server.db', (err) => {
    if (err) {
      return console.error(err.message);
    }
    console.log('Connected to the in-memory SQlite database.');
}); 


app.get("/", (req, res) => {
    //res.json({ success: true});
    res.json({data});
});

app.listen( 3200, function(req,res){
    console.log("The server has been connect by port : 3200");
});


