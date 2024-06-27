from flask import Flask
from flask import jsonify
from json import loads
import os
from requests import get
from time import time
import datetime
from bs4 import BeautifulSoup as bs

app = Flask(__name__)


@app.route('/')
def index():
    return jsonify(loads(get("https://www.flightradar24.com/v1/search/web/find?query=RAM&limit=100",headers={'User-Agent': 'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:20.0) Gecko/20100101 Firefox/20.0'}).content.decode()))


@app.route('/flight/<id>')
def flight(id):
    os.system(f"curl -s https://www.flightradar24.com/data/aircraft/{id} > info.html")
    with open("info.html","r",encoding="utf-8") as f:
        html = bs(f.read(),'html.parser')
    r =list()
    for x in html.find_all(class_="data-row"):
        info = x.find_all(class_="hidden-sm")[2:]
        if("estimated" not in info[-3].text.lower()):
            break
        d = dict()
        d["Id"]=0
        d["Ref"] = info[2].text.strip()
        d["Depart"] = datetime.datetime.fromtimestamp(int(info[4]["data-timestamp"]))
        d["Arrive"] = datetime.datetime.fromtimestamp(int(info[6]["data-timestamp"]))
        d["Plane"] = None
        d["SessionPilot"] = None
        d["From"] = {"Id":0,"Code":info[0].find('a').text.strip()[1:-1],"City":None,"Country":None}
        d["To"] = {"Id":0,"Code":info[1].find('a').text.strip()[1:-1],"City":None,"Country":None}
        d["TotalHeures"] = (int(info[6]["data-timestamp"])-int(info[4]["data-timestamp"]))/3600
        r.append(d)
    return jsonify(r[-1::-1])


@app.route('/departure/<id>')
def departure(id):
    t = str(int(time()))
    result = loads(get(f"https://api.flightradar24.com/common/v1/airport.json?code={id}&plugin[]=&plugin-setting[schedule][mode]=departures&plugin-setting[schedule][timestamp]={t}&page=1&limit=100&fleet=&token=",headers={'User-Agent': 'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:20.0) Gecko/20100101 Firefox/20.0'}).content.decode())["result"]["response"]["airport"]["pluginData"]["schedule"]["departures"]
    #totalPage = int(result["page"]["total"])-1
    result = result["data"]
    #for i in range(1,totalPage):
    #    result.extend(loads(get(f"https://api.flightradar24.com/common/v1/airport.json?code={id}&plugin[]=&plugin-setting[schedule][mode]=departures&plugin-setting[schedule][timestamp]={t}&page={i}&limit=100&fleet=&token=",headers={'User-Agent': 'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:20.0) Gecko/20100101 Firefox/20.0'}).content.decode())["result"]["response"]["airport"]["pluginData"]["schedule"]["departures"]["data"])
    r = []
    for i in result :
        if  i["flight"]["aircraft"]["registration"] is not None and i["flight"]["aircraft"]["registration"]!='' :
            d =dict()
            d["Id"]=0
            d["Ref"] = i["flight"]["identification"]["number"]["default"]
            d["Depart"] = datetime.datetime.fromtimestamp(i["flight"]["time"]["scheduled"]["departure"])
            d["Arrive"] = datetime.datetime.fromtimestamp(i["flight"]["time"]["scheduled"]["arrival"])
            d["Plane"] = i["flight"]["aircraft"]["registration"]
            d["SessionPilot"] = None
            d["From"] = None
            d["To"] = {"Id":0,"Code":i["flight"]["airport"]["destination"]["code"]["iata"],"City":i["flight"]["airport"]["destination"]["position"]["region"]["city"],"Country":i["flight"]["airport"]["destination"]["position"]["country"]["name"]}
            d["TotalHeures"] = (i["flight"]["time"]["scheduled"]["arrival"]-i["flight"]["time"]["scheduled"]["departure"])/3600

            r.append(d)
    return jsonify(r)




