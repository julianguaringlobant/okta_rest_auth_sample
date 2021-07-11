#include <cpr/cpr.h>
#include <iostream>
#include <nlohmann/json.hpp>

using json = nlohmann::json;


int main(int argc, char ** argv)
{
    std::cout << "Basic Authentication test sample with OKTA" << std::endl;

    if (argc < 4)
    {
        std::cout << "Usage: auth domainname username password\n";
        return -1;
    }

    auto data = json{};
    data["username"] = argv[2];
    data["password"] = argv[3];

    std::string datastring = data.dump();

    auto urlstring = std::string{"https://"} + std::string{argv[1]} + std::string{".okta.com"}  ;

    auto authn_api = std::string{"/api/v1/authn"};
    cpr::Response r = cpr::Post(
            cpr::Url{ urlstring + authn_api},
            cpr::Body{datastring},
            cpr::Header{
                {"Accept", "application/json"},
                {"Content-Type", "application/json"}
            }
            );


    std::cout << "STATUS CODE : " << r.status_code << std::endl;                  // 200
    std::cout << "+++++++++++++++++++++++++++++++++++++++++" << std::endl;
    std::cout << std::endl << "CONTENT TYPE: " << r.header["content-type"] << std::endl;       // application/json; charset=utf-8
    std::cout << "+++++++++++++++++++++++++++++++++++++++++" << std::endl;

    auto jsonresponse = json::parse(r.text);
    std::cout << jsonresponse.dump(4) << std::endl;

    std::cout << "+++++++++++++++++++++++++++++++++++++++++" << std::endl;

    auto statusString = jsonresponse["status"];
    std::cout << "Status String is : " << statusString << std::endl;


    if ( statusString == "MFA_REQUIRED")
    {

        std::cout << "Multi Factor Authentication Required, the following factors are available for this user: " << std::endl;
        auto factors = jsonresponse["_embedded"]["factors"];
        for (auto& factor : factors)
        {
            std::cout << "ID: " << factor["id"] << std::endl;
            std::cout << "TYPE: " << factor["factorType"] << std::endl;

        }
        std::cout << "+++++++++++++++++++++++++++++++++++++++++" << std::endl;
        for (auto i = 0; i < 15; ++i) std::cout << std::endl;
        for (auto& factor : factors)
        {
            if (factor["factorType"] != "push") continue;
            std::cout << "Push Factor ID: " << factor["id"] << std::endl;
            std::cout << "Profile: " << std::endl;
            std::cout << factor["profile"].dump(4) << std::endl;

            auto verifydata = json{};
            verifydata["stateToken"] = jsonresponse["stateToken"];
            std::string verifydatastring = verifydata.dump();
            cpr::Response verifyresponse = cpr::Post(
                    cpr::Url{ factor["_links"]["verify"]["href"]},
                    cpr::Body{verifydatastring},
                    cpr::Header{
                            {"Accept", "application/json"},
                            {"Content-Type", "application/json"}
                    }
            );
            std::cout << "STATUS CODE : " << verifyresponse.status_code << std::endl;                  // 200
            std::cout << "+++++++++++++++++++++++++++++++++++++++++" << std::endl;
            std::cout << std::endl << "CONTENT TYPE: " << verifyresponse.header["content-type"] << std::endl;       // application/json; charset=utf-8
            std::cout << "+++++++++++++++++++++++++++++++++++++++++" << std::endl;

            auto jsonverifyresponse = json::parse(verifyresponse.text);
            std::cout << jsonverifyresponse.dump(4) << std::endl;

            std::cout << "Break here!!!! " << std::endl;

            verifyresponse = cpr::Post(
                    cpr::Url{ factor["_links"]["verify"]["href"]},
                    cpr::Body{verifydatastring},
                    cpr::Header{
                            {"Accept", "application/json"},
                            {"Content-Type", "application/json"}
                    }
            );

            jsonverifyresponse = json::parse(verifyresponse.text);
            std::cout << jsonverifyresponse.dump(4) << std::endl;

        }
    }

    return 0;

}
