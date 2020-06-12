trigger = function()
    return  false
end

occur_days = function ()
    return 2
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            print("EVENT_TEST".."_OPTION_1_DESC selected")
        end
    }
}