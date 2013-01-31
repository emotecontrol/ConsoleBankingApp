﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    class StreetAddressConverter
    {
        // list of abbreviations adapted from:
        // https://www.usps.com/send/official-abbreviations.htm#2

        List<string[]> postalList = new List<string[]>()
        {
            new string[3] {"alley", "ally", "aly"},
            new string[3] {"annex", "anex", "anx"},
            new string[3] {"arcade", "arcade", "arc"},
            new string[3] {"avenue", "av", "ave"},
            new string[3] {"avenue", "avn", "ave"},
            new string[3] {"avenue", "avenu", "ave"},
            new string[3] {"avenue", "avnue", "ave"},
            new string[3] {"bayoo", "bayou", "byu"},
            new string[3] {"beach", "bch", "bch"},
            new string[3] {"bend", "bnd", "bnd"},
            new string[3] {"bluff", "bluf", "blf"},
            new string[3] {"bluffs", "bluffs", "blfs"},
            new string[3] {"bottom", "bot", "btm"},
            new string[3] {"bottom", "bottm", "btm"},
            new string[3] {"boulevard", "boul", "blvd"},
            new string[3] {"boulevard", "boulv", "blvd"},
            new string[3] {"branch", "brnch", "br"},
            new string[3] {"bridge", "brdge", "brg"},
            new string[3] {"brook", "brk", "brk"},
            new string[3] {"brooks", "brks", "brks"},
            new string[3] {"burg", "burg", "bg"},
            new string[3] {"burgs", "bgs", "bgs"},
            new string[3] {"bypass", "bypa", "byp"},
            new string[3] {"bypass", "bypas", "byp"},
            new string[3] {"bypass", "byps", "byp"},
            new string[3] {"camp", "cmp", "cp"},
            new string[3] {"canyon", "canyn", "cyn"},
            new string[3] {"canyon", "cnyn", "cyn"},
            new string[3] {"cape", "cpe", "cpe"},
            new string[3] {"causeway", "causway", "cswy"},
            new string[3] {"centre", "cen", "ctr"},
            new string[3] {"centre", "cent", "ctr"},
            new string[3] {"centre", "centr", "ctr"},
            new string[3] {"centre", "center", "ctr"},
            new string[3] {"centre", "cnter", "ctr"},
            new string[3] {"centre", "cntr", "ctr"},
            new string[3] {"centres", "centers", "ctrs"},
            new string[3] {"circle", "circ", "cir"},
            new string[3] {"circle", "circl", "cir"},
            new string[3] {"circle", "crcle", "cir"},
            new string[3] {"circles", "circles", "cirs"},
            new string[3] {"cliff", "clf", "clf"},
            new string[3] {"cliffs", "clfs", "clfs"},
            new string[3] {"club", "clb", "clb"},
            new string[3] {"common", "cmn", "cmn"},
            new string[3] {"corner", "cor", "cor"},
            new string[3] {"corners", "cors", "cors"},
            new string[3] {"course", "crse", "crse"},
            new string[3] {"court", "crt", "ct"},
            new string[3] {"courts", "cts", "cts"},
            new string[3] {"cove", "cv", "cv"},
            new string[3] {"coves", "cvs", "cvs"},
            new string[3] {"creek", "ck", "crk"},
            new string[3] {"creek", "cr", "crk"},
            new string[3] {"crescent", "crecent", "cres"},
            new string[3] {"crescent", "crscnt", "cres"},
            new string[3] {"crescent", "crsent", "cres"},
            new string[3] {"crescent", "crsnt", "cres"},
            new string[3] {"crest", "crst", "crst"},
            new string[3] {"crossing", "crssing", "xing"},
            new string[3] {"crossing", "crssng", "xing"},
            new string[3] {"crossroad", "crsrd", "xrd"},
            new string[3] {"curve", "crv", "curv"},
            new string[3] {"dale", "dl", "dl"},
            new string[3] {"dam", "dm", "dm"},
            new string[3] {"divide", "div", "dv"},
            new string[3] {"divide", "dvd", "dv"},
            new string[3] {"drive", "driv", "dr"},
            new string[3] {"drive", "drv", "dr"},
            new string[3] {"drives", "drives", "drs"},
            new string[3] {"estate", "est", "est"},
            new string[3] {"estates", "ests", "ests"},
            new string[3] {"expressway", "exp", "expy"},
            new string[3] {"expressway", "expr", "expy"},
            new string[3] {"expressway", "express", "expy"},
            new string[3] {"expressway", "expw", "expy"},
            new string[3] {"extension", "extn", "ext"},
            new string[3] {"extension", "extnsn", "ext"},
            new string[3] {"extensions", "exts", "exts"},
            new string[3] {"fall", "fl", "fall"},
            new string[3] {"falls", "fls", "fls"},
            new string[3] {"ferry", "frry", "fry"},
            new string[3] {"field", "fld", "fld"},
            new string[3] {"fields", "flds", "flds"},
            new string[3] {"flat", "flt", "flt"},
            new string[3] {"flats", "flts", "flts"},
            new string[3] {"ford", "frd", "frd"},
            new string[3] {"fords", "frds", "frds"},
            new string[3] {"forest", "forests", "frst"},
            new string[3] {"forge", "forg", "frg"},
            new string[3] {"forges", "frgs", "frgs"},
            new string[3] {"fork", "frk", "frk"},
            new string[3] {"forks", "frks", "frks"},
            new string[3] {"fort", "frt", "ft"},
            new string[3] {"freeway", "freewy", "fwy"},
            new string[3] {"freeway", "frway", "fwy"},
            new string[3] {"freeway", "frwy", "fwy"},
            new string[3] {"garden", "gardn", "gdn"},
            new string[3] {"garden", "grden", "gdn"},
            new string[3] {"garden", "grdn", "gdn"},
            new string[3] {"gardens", "grdns", "gdns"},
            new string[3] {"gateway", "gatewy", "gtwy"},
            new string[3] {"gateway", "gatway", "gtwy"},
            new string[3] {"glen", "gln", "gln"},
            new string[3] {"glens", "glns", "glns"},
            new string[3] {"green", "grn", "grn"},
            new string[3] {"greens", "grns", "grns"},
            new string[3] {"grove", "grov", "grv"},
            new string[3] {"groves", "grvs", "grvs"},
            new string[3] {"harbor", "harb", "hbr"},
            new string[3] {"harbor", "harbr", "hbr"},
            new string[3] {"harbor", "hrbor", "hbr"},
            new string[3] {"harbors", "hrbors", "hbrs"},
            new string[3] {"haven", "havn", "hvn"},
            new string[3] {"heights", "height", "hts"},
            new string[3] {"heights", "hgts", "hts"},
            new string[3] {"heights", "ht", "hts"},
            new string[3] {"highway", "highwy", "hwy"},
            new string[3] {"highway", "hiway", "hwy"},
            new string[3] {"highway", "hiwy", "hwy"},
            new string[3] {"highway", "hway", "hwy"},
            new string[3] {"hill", "hl", "hl"},
            new string[3] {"hills", "hls", "hls"},
            new string[3] {"hollow", "hllw", "holw"},
            new string[3] {"hollows", "holws", "holws"},
            new string[3] {"inlet", "inlt", "inlt"},
            new string[3] {"island", "islnd", "is"},
            new string[3] {"islands", "islnds", "iss"},
            new string[3] {"isle", "isles", "isle"},
            new string[3] {"junction", "jction", "jct"},
            new string[3] {"junction", "jctn", "jct"},
            new string[3] {"junction", "junctn", "jct"},
            new string[3] {"junction", "juncton", "jct"},
            new string[3] {"junctions", "jctns", "jcts"},
            new string[3] {"key", "ky", "ky"},
            new string[3] {"keys", "kys", "kys"},
            new string[3] {"knoll", "knol", "knl"},
            new string[3] {"knolls", "knls", "knls"},
            new string[3] {"lake", "lk", "lk"},
            new string[3] {"lakes", "lks", "lks"},
            new string[3] {"land", "land", "land"},
            new string[3] {"landing", "lndng", "lndg"},
            new string[3] {"lane", "lanes", "ln"},
            new string[3] {"light", "lgt", "lgt"},
            new string[3] {"lights", "lgts", "lgts"},
            new string[3] {"loaf", "lf", "lf"},
            new string[3] {"lock", "lck", "lck"},
            new string[3] {"locks", "lcks", "lcks"},
            new string[3] {"lodge", "ldge", "ldg"},
            new string[3] {"lodge", "lodg", "ldg"},
            new string[3] {"loop", "loops", "loop"},
            new string[3] {"mall", "mall", "mall"},
            new string[3] {"manor", "mr", "mnr"},
            new string[3] {"manors", "mnrs", "mnrs"},
            new string[3] {"meadow", "mdw", "mdw"},
            new string[3] {"meadows", "medows", "mdws"},
            new string[3] {"mews", "mew", "mews"},
            new string[3] {"mill", "ml", "ml"},
            new string[3] {"mills", "mls", "mls"},
            new string[3] {"mission", "missn", "msn"},
            new string[3] {"mission", "mssn", "msn"},
            new string[3] {"motorway", "mtwy", "mtwy"},
            new string[3] {"mount", "mnt", "mt"},
            new string[3] {"mountain", "mntain", "mtn"},
            new string[3] {"mountain", "mntn", "mtn"},
            new string[3] {"mountain", "mountin", "mtn"},
            new string[3] {"mountain", "mtin", "mtn"},
            new string[3] {"mountains", "mntns", "mtns"},
            new string[3] {"neck", "nck", "nck"},
            new string[3] {"orchard", "orchrd", "orch"},
            new string[3] {"oval", "ovl", "oval"},
            new string[3] {"overpass", "opas", "opas"},
            new string[3] {"park", "pk", "park"},
            new string[3] {"park", "prk", "park"},
            new string[3] {"park", "parks", "park"},
            new string[3] {"parkway", "parkwy", "pkwy"},
            new string[3] {"parkway", "pkway", "pkwy"},
            new string[3] {"parkway", "pky", "pkwy"},
            new string[3] {"parkways", "pkwys", "pkwys"},
            new string[3] {"pass", "pass", "pass"},
            new string[3] {"passage", "psge", "psge"},
            new string[3] {"path", "paths", "path"},
            new string[3] {"pike", "pikes", "pike"},
            new string[3] {"pine", "pine", "pne"},
            new string[3] {"pines", "pnes", "pnes"},
            new string[3] {"place", "pl", "pl"},
            new string[3] {"plain", "pln", "pln"},
            new string[3] {"plains", "plaines", "plns"},
            new string[3] {"plaza", "plza", "plz"},
            new string[3] {"point", "pt", "pt"},
            new string[3] {"points", "pts", "pts"},
            new string[3] {"port", "prt", "prt"},
            new string[3] {"ports", "prts", "prts"},
            new string[3] {"prairie", "prarie", "pr"},
            new string[3] {"prairie", "prr", "pr"},
            new string[3] {"radial", "rad", "radl"},
            new string[3] {"radial", "radiel", "radl"},
            new string[3] {"ramp", "ramp", "ramp"},
            new string[3] {"ranch", "ranches", "rnch"},
            new string[3] {"ranch", "rnchs", "rnch"},
            new string[3] {"rapid", "rpd", "rpd"},
            new string[3] {"rapids", "rpds", "rpds"},
            new string[3] {"rest", "rst", "rst"},
            new string[3] {"ridge", "rdge", "rdg"},
            new string[3] {"ridges", "rdges", "rdgs"},
            new string[3] {"river", "rivr", "riv"},
            new string[3] {"river", "rvr", "riv"},
            new string[3] {"road", "rd", "rd"},
            new string[3] {"roads", "rds", "rds"},
            new string[3] {"route", "rte", "rte"},
            new string[3] {"row", "row", "row"},
            new string[3] {"rue", "rue", "rue"},
            new string[3] {"run", "run", "run"},
            new string[3] {"shoal", "shl", "shl"},
            new string[3] {"shoals", "shls", "shls"},
            new string[3] {"shore", "shoar", "shr"},
            new string[3] {"shores", "shoars", "shrs"},
            new string[3] {"skyway", "skwy", "skwy"},
            new string[3] {"spring", "spng", "spg"},
            new string[3] {"springs", "spngs", "spgs"},
            new string[3] {"springs", "sprngs", "spgs"},
            new string[3] {"spur", "spur", "spur"},
            new string[3] {"spurs", "spurs", "spurs"},
            new string[3] {"square", "sqr", "sq"},
            new string[3] {"square", "squ", "sq"},
            new string[3] {"squares", "sqrs", "sqs"},
            new string[3] {"station", "statn", "sta"},
            new string[3] {"station", "stn", "sta"},
            new string[3] {"stravenue", "strav", "stra"},
            new string[3] {"stravenue", "strave", "stra"},
            new string[3] {"stravenue", "straven", "stra"},
            new string[3] {"stravenue", "stravn", "stra"},
            new string[3] {"stravenue", "strvnue", "stra"},
            new string[3] {"stream", "streme", "strm"},
            new string[3] {"street", "str", "st"},
            new string[3] {"street", "strt", "st"},
            new string[3] {"streets", "strts", "sts"},
            new string[3] {"summit", "sumit", "smt"},
            new string[3] {"summit", "sumitt", "smt"},
            new string[3] {"terrace", "terr", "ter"},
            new string[3] {"throughway", "trwy", "trwy"},
            new string[3] {"trace", "traces", "trce"},
            new string[3] {"track", "tracks", "trak"},
            new string[3] {"track", "trk", "trak"},
            new string[3] {"track", "trks", "trak"},
            new string[3] {"trafficway", "trfy", "trfy"},
            new string[3] {"trail", "trails", "trl"},
            new string[3] {"trail", "trls", "trl"},
            new string[3] {"tunnel", "tunel", "tunl"},
            new string[3] {"tunnel", "tunls", "tunl"},
            new string[3] {"tunnel", "tunnels", "tunl"},
            new string[3] {"turnpike", "trnpk", "tpke"},
            new string[3] {"turnpike", "trpk", "tpke"},
            new string[3] {"turnpike", "turnpk", "tpke"},
            new string[3] {"underpass", "upas", "upas"},
            new string[3] {"union", "un", "un"},
            new string[3] {"unions", "uns", "uns"},
            new string[3] {"valley", "vally", "vly"},
            new string[3] {"valley", "vlly", "vly"},
            new string[3] {"valleys", "vallys", "vlys"},
            new string[3] {"viaduct", "vdct", "via"},
            new string[3] {"viaduct", "viadct", "via"},
            new string[3] {"view", "vw", "vw"},
            new string[3] {"views", "vws", "vws"},
            new string[3] {"village", "vill", "vlg"},
            new string[3] {"village", "villag", "vlg"},
            new string[3] {"village", "villg", "vlg"},
            new string[3] {"village", "villiage", "vlg"},
            new string[3] {"villages", "villiages", "vlgs"},
            new string[3] {"ville", "vl", "vl"},
            new string[3] {"vista", "vist", "vis"},
            new string[3] {"vista", "vst", "vis"},
            new string[3] {"vista", "vsta", "vis"},
            new string[3] {"walk", "walk", "walk"},
            new string[3] {"walks", "walks", "walks"},
            new string[3] {"wall", "wall", "wall"},
            new string[3] {"way", "wy", "way"},
            new string[3] {"ways", "wys", "way"},
            new string[3] {"well", "wl", "wl"},
            new string[3] {"wells", "wls", "wls"}

        };
        
        //public void initList()
        //{
        //    for (int i = 0; i < postalArray.Length; i++)
        //    {
        //        postal.Add(postalArray[i]);
        //    }
        //}

        //public List<string[]> initList(){
            
        //    for (int i=0; i<= 100; i++){
        //    postal[i] = postalArray.Clone();
        //    }
        public StreetAddressConverter()
        {
            //initList();
        }
        
        //private bool matchInPostal(string[] abbr, string findMe){
        //    findMe = findMe.ToLower();
        //    if (abbr[0] ==  findMe || abbr[1] == findMe || abbr[2] == findMe)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public string convertToAbbr(string address)
        {
            address = address.ToLower();
            string[] addressResult = postalList.Find(delegate (string [] p){
                return (p[0] == address || p[1] == address || p[2] == address);
            });
            if (addressResult !=null)
            {
                return addressResult[2];
            }
            else
            {
                return address;
            }
        }
        
        public string convertToAddress(string address)
        {
            address = address.ToLower();
            string[] addressResult = postalList.Find(delegate (string [] p){
                return (p[0] == address || p[1] == address || p[2] == address);
            });
            if (addressResult !=null)
            {
                return addressResult[0];
            }
            else
            {
                return address;
            }
        }
    }
}




         


 