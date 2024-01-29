import { lineData } from "@/interfaces/lineData"

export interface topicDTO {
    topic: string,
    projectCount: number
}

export interface timedDataDTO {
    bucketDateLabel: string,
    topics: topicDTO[]
}

/***
 * Coverts timed data DTO into lineData
 * @param data - timedDataDTO object to be  converted
 * @returns the lineData object
 */

export function convertTimedData(data: timedDataDTO[],){
    var convertedData : lineData[] = []
    for(var i = 0; i < data.length; i++){
        var date = data[i].bucketDateLabel;
        var lineData : lineData = {date: date, topic0: 0, topic0Name: "", topic1: 0, topic1Name: "", topic2: 0, topic2Name: "", topic3: 0, topic3Name: "", topic4: 0, topic4Name: ""}

        for(var j = 0; j < data[i].topics.length; j++){
            var topic = data[i].topics[j];
            lineData["topic" + (j)] = topic.projectCount;
            lineData["topic" + (j) + "Name"] = topic.topic;
        }
        convertedData.push(lineData)
    }    
    console.log(convertedData);
    return convertedData;
}
/**
 * Gets topic for labels from timeData dto
 * @param data  - timedData dto
 * @returns string[] list of labels
 */

export function getLabels(data: timedDataDTO[]){
    var labels : string[] = [];
    for(var i = 0; i < data[0].topics.length; i++){
        labels.push(data[0].topics[i].topic);
    }
    return labels;
   
}